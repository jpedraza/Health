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

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Kernel.GetAll(serviceType);
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
        /// <param name="constructorParameters">Параметры конструктора.</param>
        /// <returns>Экземпляр объекта.</returns>
        public object Get(Type type, params object[] constructorParameters)
        {
            if (constructorParameters == null || constructorParameters.Length == 0) return Get(type);
            var parameters = new ConstructorArgument[constructorParameters.Length];
            var types = new List<Type>();
            foreach (object constructorParameter in constructorParameters)
            {
                types.Add(constructorParameter.GetType());
            }
            ConstructorInfo constructorInfo = type.GetConstructor(types.ToArray());
            if (constructorInfo == null) throw new Exception("Не найден конструктор.");
            ParameterInfo[] cstrParams = constructorInfo.GetParameters();
            for (int i = 0; i < constructorParameters.Length; i++)
            {
                object constructorParameter = constructorParameters[i];
                parameters[i] = new ConstructorArgument(cstrParams[i].Name, constructorParameter);
            }
            return Kernel.Get(type, parameters);
        }

        #endregion
    }
}