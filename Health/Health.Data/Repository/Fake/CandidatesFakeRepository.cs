using System;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Ninject;

namespace Health.Data.Repository.Fake
{
    public sealed class CandidatesFakeRepository<TCandidate> : CoreFakeRepository<TCandidate, ICandidate>,
                                                               ICandidateRepository<ICandidate>
        where TCandidate : class, ICandidate, new()
    {
        public CandidatesFakeRepository()
        {
            
        }

        public override void InitializeData()
        {
            Save(new TCandidate
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "cand1",
                LastName = "cand1",
                Login = "cand1",
                Password = "cand1",
                Policy = "some policy number",
                Role = DefaultCandidateRole,
                ThirdName = "cand1"
            });
            Save(new TCandidate
            {
                Birthday = new DateTime(1980, 12, 2),
                Card = "some card number",
                FirstName = "cand2",
                LastName = "cand2",
                Login = "cand2",
                Password = "cand2",
                Policy = "some policy number",
                Role = DefaultCandidateRole,
                ThirdName = "cand2"
            });
        }

        public override bool Save(ICandidate entity)
        {
            entity.Role = DefaultCandidateRole;
            return base.Save(entity);
        }

        public IRole DefaultCandidateRole
        {
            get
            {
                var role = Instance<IRole>();
                role.Name = "Patient";
                role.Code = 3;
                return role;
            }
            set { }
        }
    }
}