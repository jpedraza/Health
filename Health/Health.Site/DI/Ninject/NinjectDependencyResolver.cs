using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;

namespace Health.Site.DI.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IDIKernel _kernel;

        public  NinjectDependencyResolver(IDIKernel kernel)
        {
            _kernel = kernel;
        }

        #region Implementation of IDependencyResolver

        /// <summary>
        /// Разрешает однократно зарегистрированные службы, поддерживающие создание произвольных объектов.
        /// </summary>
        /// <returns>
        /// Запрошенная служба или объект.
        /// </returns>
        /// <param name="service_type">Тип запрошенной службы или объекта.</param>
        public object GetService(Type service_type)
        {
            return _kernel.Get(service_type);
        }

        /// <summary>
        /// Разрешает многократно зарегистрированные службы.
        /// </summary>
        /// <returns>
        /// Запрошенные службы.
        /// </returns>
        /// <param name="service_type">Тип запрашиваемых служб.</param>
        public IEnumerable<object> GetServices(Type service_type)
        {
            try
            {
                return _kernel.GetServices(service_type);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        #endregion
    }
}