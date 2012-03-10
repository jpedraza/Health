using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Windows.Forms;
using EFCFModel;
using Ninject;
using PrototypeHM.DI;
using PrototypeHM.Forms;

namespace PrototypeHM
{
    internal class YApplication : ApplicationContext
    {
        private readonly IKernel _kernel;

        internal YApplication()
        {
#if Release
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ApplicationThreadException;
#endif
            _kernel = new StandardKernel();
            Bind();
            MainForm = _kernel.Get<DIMainForm>();
            MainForm.Show();
        }

        internal void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            YMessageBox.Error(e.Exception.Message);
        }

        internal void Bind()
        {
            // General
            _kernel.Bind<IDIKernel>().To<DIKernel>();
            _kernel.Bind<DbContext>().To<EFHealthContext>().InThreadScope();
            _kernel.Bind<ISchemaManager>().To<ObjectContextSchemaManager>().InThreadScope()
                .WithConstructorArgument("context", ((IObjectContextAdapter) _kernel.Get<DbContext>()).ObjectContext);
            _kernel.Bind<ByteConverter>().ToSelf().InThreadScope();
            _kernel.Bind<DIMainForm>().ToSelf().InThreadScope();
            _kernel.Bind<IValidator>().To<Validator>().InThreadScope();
        }
    }
}