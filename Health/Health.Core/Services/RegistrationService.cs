using Health.API.Entities;
using Health.API.Services;

namespace Health.Core.Services
{
    /// <summary>
    /// ������ �����������.
    /// </summary>
    /// <typeparam name="TCandidate">��� ���������.</typeparam>
    public class RegistrationService<TCandidate> : CoreService, IRegistrationService<ICandidate>
        where TCandidate : class, ICandidate, new()
    {
        #region IRegistrationService<ICandidate> Members

        /// <summary>
        /// ������� ������.
        /// </summary>
        /// <param name="candidate">��������.</param>
        public void AcceptBid(ICandidate candidate)
        {
        }

        /// <summary>
        /// ��������� ������.
        /// </summary>
        /// <param name="candidate">��������.</param>
        public void SaveBid(ICandidate candidate)
        {
            CoreKernel.CandRepo.Save(candidate);
        }

        /// <summary>
        /// ��������� ������.
        /// </summary>
        /// <param name="candidate">��������.</param>
        public void RejectBid(ICandidate candidate)
        {
            CoreKernel.CandRepo.Delete(candidate);
        }

        #endregion
    }
}