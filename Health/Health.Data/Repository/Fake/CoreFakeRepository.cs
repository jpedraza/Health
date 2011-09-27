using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Data.Repository.Fake
{
    public abstract class CoreFakeRepository<TIEntity> : Core.Core, ICoreRepository<TIEntity>
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
            if (entity is IKey)
            {
                var max_id = 0;
                foreach (IKey i_entity in _entities)
                {
                    max_id = i_entity.Id > max_id ? i_entity.Id : max_id;
                }
                ((IKey) entity).Id = max_id + 1;
            }
            _entities.Add(entity);
            return true;
        }

        public virtual bool Delete(TIEntity entity)
        {
            _entities.Remove(entity);
            return true;
        }

        public virtual bool Update(TIEntity entity)
        {
            if (entity is IKey)
            {
                var key_entity = entity as IKey;
                for (int i = 0; i < _entities.Count; i++)
                {
                    var i_entity = (IKey)_entities[i];
                    if (key_entity.Id == i_entity.Id)
                    {
                        _entities[i] = entity;
                        return true;
                    }
                }   
            }
            for (int i = 0; i < _entities.Count; i++)
            {
                TIEntity i_entity = _entities[i];
                if (i_entity.Equals(entity))
                {
                    _entities[i] = entity;
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}