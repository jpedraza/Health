using Health.Core.API;
using Health.Core.API.Services;

namespace Health.Core.Services
{
    /// <summary>
    /// Базовый сервис реализующий общий функцилнал для всех сервисов.
    /// </summary>
    public class CoreService : Core, ICoreService
    {
        protected CoreService(IDIKernel diKernel) : base(diKernel)
        {
        }

        /// <summary>
        /// Получить экземпляр по интерфейсу.
        /// </summary>
        /// <typeparam name="T">Интерфейс.</typeparam>
        /// <returns>Экземпляр.</returns>
        protected T Get<T>()
        {
            return DIKernel.Get<T>();
        }
    }
}