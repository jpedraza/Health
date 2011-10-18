using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Health.Core.Entities.POCO;

namespace Health.Core.API.Repository
{
    public interface ISpecialtyRepository : ICoreRepository<Specialty>
    {
        Specialty GetById(int specialtyId);
    }
}
