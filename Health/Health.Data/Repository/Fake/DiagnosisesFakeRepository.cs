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
    public sealed class DiagnosisesFakeRepository:CoreFakeRepository<Diagnosis>,IDiagnosisRepository
    {
        public DiagnosisesFakeRepository(IDIKernel di_kernel):base(di_kernel)
        {
            #region Fake Data

            this.Save(new Diagnosis() { 
            Id = 0,
            Code = "",
            Name = ""
            });

            this.Save(new Diagnosis()
            {
                Id = 1,
                Code = "",
                Name = ""
            });

            this.Save(new Diagnosis()
            {
                Id = 2,
                Code = "",
                Name = ""
            });

            this.Save(new Diagnosis()
            {
                Id = 3,
                Code = "",
                Name = ""
            });
            this.Save(new Diagnosis()
            {
                Id = 4,
                Code = "",
                Name = ""
            });
            #endregion
        }

        #region members
        public Diagnosis GetById(int id)
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

        public Diagnosis GetByCode(string code)
        {
            try
            {
                Diagnosis returnValue = null;
                foreach (var diagnosis in base._entities)
                {
                    if (diagnosis.Code == code)
                    {
                        returnValue = diagnosis;
                        break;
                    }
                }
                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        public IList<Diagnosis> GetAllDiagnosises()
        {
            return base._entities;
        }

        public bool Add(Diagnosis newDiagnosis)
        {
            try { base._entities.Add(newDiagnosis); return true; }
            catch { return false; }
        }

        public bool Edit(Diagnosis editingDiagnosis)
        {
            try
            {
                for (var i = 0; i < base._entities.Count; i++)
                {
                    if (base._entities[i].Id == editingDiagnosis.Id)
                    {
                        base._entities[i] = editingDiagnosis;
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

        public bool DeleteByExamp(Diagnosis deleteDiagnosis)
        {
            try
            {
                base._entities.Remove(deleteDiagnosis);
                return true;

            }
            catch { return false; }
        }
        #endregion
    }
}
