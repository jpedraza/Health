using System.Collections.Generic;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;

namespace Health.Data.Repository.Fake
{
    public abstract class CoreFakeRepository<TIEntity> : Core, ICoreRepository<TIEntity>
        where TIEntity : IEntity
    {
        protected IList<TIEntity> _entities;

        protected CoreFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            _entities = new List<TIEntity>();
        }

        #region ICoreRepository<TIEntity> Members

        public virtual IEnumerable<TIEntity> GetAll()
        {
            return _entities;
        }

        public virtual bool Save(TIEntity entity)
        {
            _entities.Add(entity);
            return true;
        }

        public virtual bool Delete(TIEntity entity)
        {
            _entities.Remove(entity);
            return true;
        }

        #endregion
    }
}