using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using Health.Core.Entities.POCO.Abstract;
using System;

namespace Health.Data.Repository.Fake
{
    public sealed class SurgerysFakeRepository : CoreFakeRepository<Surgery>, ISurgeryRepository
    {
        public SurgerysFakeRepository(IDIKernel di_kernel) : base(di_kernel)
        {
            #region Create Fake Data
            this.Save(new Surgery
            {
                Id = 0,
                Name = "Пластика ДМЖП по методике двойной заплаты в условиях ИК",
                Description = "Описание процесса лечения"
            });

            this.Save(new Surgery
            {
                Id = 1,
                Name = "Пластика ДМПП, ДМ, ЖП, ИСЛА",
                Description = "Описание процесса лечения"
            });

            this.Save(new Surgery
            {
                Id = 2,
                Name = "Аномалия Эбштейна",
                Description = "Описание процесса лечения"
            });

            this.Save(new Surgery
            {
                Id = 3,
                Name = "тетрады Фалло",
                Description = "Описание процесса лечения"
            });
            #endregion
        }
                
        #region members
        public Surgery GetById(int id)
        {
            try
            {
                var returnValue = base._entities[id];
                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        public IList<Surgery> GetAllSurgerys()
        {
            return base._entities;
        }

        public bool Add(Surgery newSurgery)
        {
            try { base._entities.Add(newSurgery); return true; }
            catch { return false; }
        }

        public bool Edit(Surgery editingSurgery)
        {
            try
            {
                for (var i = 0; i < base._entities.Count; i++)
                {
                    if (base._entities[i].Id == editingSurgery.Id)
                    {
                        base._entities[i] = editingSurgery;
                        return true;
                    }
                    else
                        continue;
                }
            }
            catch { return false; }
            return true;
        }

        public bool DeleteById(int id)
        {
            try
            {
                base._entities.RemoveAt(id);
                return true;

            }
            catch { return false; }
        }

        public bool DeleteByExamp(Surgery deleteSurgery)
        {
            try
            {
                base._entities.Remove(deleteSurgery);
                return true;

            }
            catch { return false; }
        }
        #endregion
    }
}
