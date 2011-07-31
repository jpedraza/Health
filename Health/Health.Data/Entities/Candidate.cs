using System;
using Health.API.Entities;

namespace Health.Data.Entities
{
    public class Candidate : ICandidate
    {
        #region ICandidate Members

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IRole Role { get; set; }
        public DateTime Birthday { get; set; }
        public string Token { get; set; }
        public string Policy { get; set; }
        public string Card { get; set; }

        #endregion
    }
}