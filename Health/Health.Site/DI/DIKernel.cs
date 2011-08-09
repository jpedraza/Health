using System;
using Health.API;
using Health.API.Entities;
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

        /// <summary>
        /// Получить экземпляр объекта, зная его интерфейс и инициализатор.
        /// </summary>
        /// <typeparam name="TObject">Интерфейс сущности.</typeparam>
        /// <param name="init">Инициализатор объекта.</param>
        /// <returns>Сущность.</returns>
        public TObject Instance<TObject>(Action<TObject> init) where TObject : IEntity
        {
            var obj = Get<TObject>();
            init.Invoke(obj);
            return obj;
        }

        #endregion
    }
}