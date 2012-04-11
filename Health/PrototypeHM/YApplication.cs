﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Ninject;
using Prototype.DI;
using Prototype.Forms;

namespace Prototype
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
            TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
            _kernel = new StandardKernel();
            Bind();
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
            _kernel.Bind<DbContext>().To<EFHealthContext>().InTransientScope();
            _kernel.Bind<ISchemaManager>().To<ObjectContextSchemaManager>().InTransientScope()
                .WithConstructorArgument("context", ((IObjectContextAdapter) _kernel.Get<DbContext>()).ObjectContext);
            _kernel.Bind<ByteConverter>().ToSelf().InTransientScope();
            _kernel.Bind<DIMainForm>().ToSelf().InTransientScope();
            _kernel.Bind<IValidator>().To<Validator>().InTransientScope();
        }
    }
}