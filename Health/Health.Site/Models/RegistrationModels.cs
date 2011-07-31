using System;
using System.ComponentModel.DataAnnotations;
using Health.API.Entities;

namespace Health.Site.Models
{
    public class CandidateRegistrationFormModel : ICandidate
    {
        #region ICandidate Members

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ThirdName { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [NotMapped]
        public IRole Role { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [NotMapped]
        public string Token { get; set; }

        [Required]
        public string Policy { get; set; }

        [Required]
        public string Card { get; set; }

        #endregion
    }
}