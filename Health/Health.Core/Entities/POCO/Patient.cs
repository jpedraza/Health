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
        
        /*
         Здесь следует отметить следующее:
         * В ТЗ коды диагнозов и их описание разделены
         * однако это различие будет совершаться при выводе информации
         * 
         * Для избежания избыточности кода, все собрано в общие поля
         */

        /// <summary>
        /// Рекомендуемые хирургические воздействия
        /// </summary>
        public IList<Surgery> AdvisableSurgerys { get; set; }

        /// <summary>
        /// Выполненные хирургические воздействия
        /// </summary>
        public IList<Surgery> FulfilledSurgerys { get; set; }
    }
}