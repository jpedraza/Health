using System;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// 
    /// </summary>
    public class Candidate : User
    {
        /// <summary>
        /// Номер полюса.
        /// </summary>
        public string Policy { get; set; }

        /// <summary>
        /// Номер больничной карты.
        /// </summary>
        public string Card { get; set; }
    }
}