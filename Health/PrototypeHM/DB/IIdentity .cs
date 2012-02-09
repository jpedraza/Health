using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeHM.DB
{
    public interface IIdentity
    {
        /// <summary>
        /// Id сущности
        /// </summary>
        int Id { get; set; }
    }
}
