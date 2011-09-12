using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    /// <summary>
    /// Интерфейс репозитория кандидатов на регистрацию.
    /// </summary>
    public interface ICandidateRepository : ICoreRepository<Candidate>
    {
        bool DeleteById(int candidate_id);

        Candidate GetById(int candidate_id);
    }
}