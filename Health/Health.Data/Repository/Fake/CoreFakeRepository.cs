using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Data.Repository.Fake
{
    public abstract class CoreFakeRepository<TIEntity> : Core.Core, ICoreRepository<TIEntity>
        where TIEntity : IEntity
    {
        protected IList<TIEntity> _entities;

        protected CoreFakeRepository(IDIKernel diKernel) : base(diKernel)
        {
            _entities = new List<TIEntity>();
        }

        protected T Get<T>()
        {
            return DIKernel.Get<T>();
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
                var maxId = 0;
                foreach (IKey iEntity in _entities)
                {
                    maxId = iEntity.Id > maxId ? iEntity.Id : maxId;
                }
                ((IKey) entity).Id = maxId + 1;
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
                var keyEntity = entity as IKey;
                for (int i = 0; i < _entities.Count; i++)
                {
                    var iEntity = (IKey)_entities[i];
                    if (keyEntity.Id == iEntity.Id)
                    {
                        _entities[i] = entity;
                        return true;
                    }
                }   
            }
            for (int i = 0; i < _entities.Count; i++)
            {
                TIEntity iEntity = _entities[i];
                if (iEntity.Equals(entity))
                {
                    _entities[i] = entity;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<TIEntity> Find(Expression<Func<TIEntity, bool>> expression)
        {
            return _entities.Where(expression.Compile());
        }

        #endregion
    }
}