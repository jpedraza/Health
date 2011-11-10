using System;
using System.Collections;
using System.Collections.Generic;
namespace Health.Core.Entities.POCO
{
    /// <summary>
    /// Пациент.
    /// </summary>
    public class Patient : Candidate
    {
        /// <summary>
        /// Лечащий врач.
        /// </summary>
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Основной диагноз
        /// </summary>
        public Diagnosis MainDiagnosis { get; set; }

        /// <summary>
        /// Вторичные диагнозы
        /// </summary>
        public IList<Diagnosis> SecondaryDiagnosises { get; set; }                
    }
}