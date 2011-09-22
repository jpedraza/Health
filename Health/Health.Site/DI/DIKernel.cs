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

        /// <summary>
        /// Получить экземпляр объекта с определенным набором параметров конструктора.
        /// </summary>
        /// <param name="type">Тип объекта.</param>
        /// <param name="constructor_parameters">Параметры конструктора.</param>
        /// <returns>Экземпляр объекта.</returns>
        public object Get(Type type, params object[] constructor_parameters)
        {
            if (constructor_parameters == null || constructor_parameters.Length == 0) return Get(type);
            var parameters = new ConstructorArgument[constructor_parameters.Length];
            var types = new List<Type>();
            foreach (object constructor_parameter in constructor_parameters)
            {
                types.Add(constructor_parameter.GetType());
            }
            ConstructorInfo constructor_info = type.GetConstructor(types.ToArray());
            if (constructor_info == null) throw new Exception("Не найден конструктор.");
            ParameterInfo[] cstr_params = constructor_info.GetParameters();
            for (int i = 0; i < constructor_parameters.Length; i++)
            {
                object constructor_parameter = constructor_parameters[i];
                parameters[i] = new ConstructorArgument(cstr_params[i].Name, constructor_parameter);
            }
            return Kernel.Get(type, parameters);
        }

        #endregion
    }
}