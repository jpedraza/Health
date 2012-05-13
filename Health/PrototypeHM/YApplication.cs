using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Model.Entities;
using Ninject;
using Prototype.DI;
using Prototype.Forms;
using Prototype.Parameters;

namespace Prototype
{
    internal class YApplication : ApplicationContext
    {
        private readonly IKernel _kernel;

        internal YApplication()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationThreadException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
            _kernel = new StandardKernel();
            Bind();
            BindRenderer();
            MainForm = _kernel.Get<DIMainForm>();
            MainForm.Show();
        }

        private void TaskSchedulerUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            foreach (Exception exception in e.Exception.Flatten().InnerExceptions)
            {
                YMessageBox.Error(exception.Message);
            }
            e.SetObserved();
        }

        private void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            YMessageBox.Error(e.Exception.Message);
        }

        private void Bind()
        {
            // General
            _kernel.Bind<IDIKernel>().To<DIKernel>();
            _kernel.Bind<DbContext>().To<EFHealthContext>().InSingletonScope();
            _kernel.Bind<ISchemaManager>().To<ObjectContextSchemaManager>().InTransientScope()
                .WithConstructorArgument("context", ((IObjectContextAdapter) _kernel.Get<DbContext>()).ObjectContext);
            _kernel.Bind<ByteConverter>().ToSelf().InTransientScope();
            _kernel.Bind<DIMainForm>().ToSelf().InTransientScope();
            _kernel.Bind<IValidator>().To<Validator>().InTransientScope();

            #region Renderers

            _kernel.Bind<RenderFactory>().ToSelf().InThreadScope();

            _kernel.Bind<BoolRenderer>().ToSelf().InTransientScope();
            _kernel.Bind<DateTimeRenderer>().ToSelf().InTransientScope();
            _kernel.Bind<DoubleRenderer>().ToSelf().InTransientScope();
            _kernel.Bind<IntegerRenderer>().ToSelf().InTransientScope();
            _kernel.Bind<StringRenderer>().ToSelf().InTransientScope();

            #endregion
        }

        private void BindRenderer()
        {
            var factory = _kernel.Get<RenderFactory>();
            factory.Bind<BoolParameter, BoolRenderer>();
            factory.Bind<DateTimeParameter, DateTimeRenderer>();
            factory.Bind<DoubleParameter, DoubleRenderer>();
            factory.Bind<IntegerParameter, IntegerRenderer>();
            factory.Bind<StringParameter, StringRenderer>();
        }
    }
}