using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Health.Core.Entities.POCO
{
    class PatientSurgery : Surgery
    {
        /// <summary>
        /// Дата проведения операции/лечения
        /// </summary>
        public DateTime DateSurgery { get; set; }

        /// <summary>
        /// Статус выполнения операции.
        /// </summary>
        public bool Status { get; set; }
    }
}
