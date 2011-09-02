using System;
using System.Collections.Generic;
using Health.Core.API;
using Ninject;
using Ninject.Parameters;

namespace Health.Site.DI
{
    public class DIKernel : IDIKernel
    {
        public DIKernel(IKernel kernel)
        {
            Kernel = kernel;
        }

        protected IKernel Kernel { get; set; }

        #region IDIKernel Members

        /// <summary>
        /// Получить объект по его интерфейсу.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <returns>Объект заданного интерфейса.</returns>
        public TObject Get<TObject>()
        {
            return Kernel.Get<TObject>();
        }

        public object Get(Type type)
        {
            return Kernel.Get(type);
        }

        /// <summary>
        /// Инициализировать объект заданного интерфейса и передать ему DI ядро и центральный сервис.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс объекта.</typeparam>
        /// <param name="core_kernel">Центральный сервис.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        public TObject Get<TObject>(ICoreKernel core_kernel) where TObject : ICore
        {
            var obj = Kernel.Get<TObject>(new[]
                                              {
                                                  new Parameter("di_kernel", this, true),
                                                  new Parameter("core_kernel", core_kernel, true)
                                              });
            return obj;
        }

        public IEnumerable<object> GetServices(Type service_type)
        {
            try
            {
                return Kernel.GetAll(service_type);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }


        #endregion
    }
}