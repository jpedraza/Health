using System;
using System.Collections.Generic;
using System.Reflection;
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

        /// <summary>
        /// Получить объект по его интерфейсу.
        /// </summary>
        /// <param name="type">Интерфейс объекта.</param>
        /// <returns>Объект заданного интерфейса.</returns>
        public object Get(Type type)
        {
            return Kernel.Get(type);
        }

        public object Get(Type type, params object[] constructor_parameters)
        {
            if (constructor_parameters == null || constructor_parameters.Length == 0) return Get(type);
            var parameters = new ConstructorArgument[constructor_parameters.Length];
            var types = new Type[constructor_parameters.Length];
            for (int i = 0; i < constructor_parameters.Length; i++)
            {
                object constructor_parameter = constructor_parameters[i];
                types[i] = constructor_parameter.GetType();
            }
            ConstructorInfo constructor_info = type.GetConstructor(types);
            if (constructor_info == null) throw new Exception("Не найден конструктор.");
            ParameterInfo[] cstr_params = constructor_info.GetParameters();
            for (int i = 0; i < constructor_parameters.Length; i++)
            {
                object constructor_parameter = constructor_parameters[i];
                parameters[i] = new ConstructorArgument(cstr_params[i].Name, constructor_parameter);
            }
            return Kernel.Get(type, parameters);
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