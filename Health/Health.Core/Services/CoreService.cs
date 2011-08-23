using Health.Core.API;
using Health.Core.API.Services;

namespace Health.Core.Services
{
    /// <summary>
    /// Базовый сервис реализующий общий функцилнал для всех сервисов.
    /// </summary>
    public class CoreService : Core, ICoreService
    {
        protected CoreService(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
        }
    }
}