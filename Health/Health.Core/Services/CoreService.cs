using Health.API;
using Health.API.Entities;

namespace Health.Core.Services
{
    /// <summary>
    /// ������� ������ ����������� ����� ���������� ��� ���� ��������.
    /// </summary>
    public class CoreService : ICore
    {
        #region ICore Members

        /// <summary>
        /// DI ����.
        /// </summary>
        public IDIKernel DIKernel { get; set; }

        /// <summary>
        /// ����������� ���� �������.
        /// </summary>
        public ICoreKernel CoreKernel { get; set; }

        /// <summary>
        /// ������� ��������� �������� �� � ����������.
        /// </summary>
        /// <typeparam name="TInstance">��������� ��������.</typeparam>
        /// <returns>��������� ��������.</returns>
        public TInstance Instance<TInstance>()
            where TInstance : IEntity
        {
            return DIKernel.Get<TInstance>();
        }

        /// <summary>
        /// ������������� DI ���� � ���� �������.
        /// </summary>
        /// <param name="di_kernel">DI ����.</param>
        /// <param name="core_kernel">����������� �����.</param>
        public void SetKernelAndCoreService(IDIKernel di_kernel, ICoreKernel core_kernel)
        {
            DIKernel = di_kernel;
            CoreKernel = core_kernel;
            InitializeData();
        }

        /// <summary>
        /// ������������� ������ ������.
        /// </summary>
        public virtual void InitializeData()
        {
            return;
        }

        #endregion
    }
}