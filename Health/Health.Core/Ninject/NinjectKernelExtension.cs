using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;
using Ninject;

namespace Health.Core.Ninject
{
    /// <summary>
    /// ћетоды расширени€ дл€ DI €дра
    /// </summary>
    public static class NinjectKernelExtension
    {
        /// <summary>
        /// ѕолучить экземпл€р сервиса или репозитори€ и произвести инициализацию доступа к
        /// DI €дру и центральному €дру системы
        /// </summary>
        /// <typeparam name="T">»нтерфейс сервиса или репозитори€</typeparam>
        /// <param name="kernel">DI €дро</param>
        /// <param name="core_service">÷ентральное €дро системы</param>
        /// <returns>Ёкземпл€р реализующий интерфейс T</returns>
        public static T Get<T>(this IKernel kernel, ICoreKernel core_service)
            where T : ICore
        {
            var t = kernel.Get<T>();
            t.SetKernelAndCoreService(kernel, core_service);
            return t;
        }
    }
}