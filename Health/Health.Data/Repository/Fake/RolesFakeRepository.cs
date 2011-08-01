using System.Collections.Generic;
using System.Linq;
using Health.API.Entities;
using Health.API.Repository;

namespace Health.Data.Repository.Fake
{
    public sealed class RolesFakeRepository<TRole> : CoreFakeRepository<TRole, IRole>, IRoleRepository<IRole>
        where TRole : class, IRole, new()
    {
        #region IRoleRepository<IRole> Members

        public IRole GetByName(string name)
        {
            return _entities.Where(x => x.Name == name).FirstOrDefault();
        }

        public override void InitializeData()
        {
            _entities = new List<IRole>
                            {
                                new TRole
                                    {
                                        Name = "Guest",
                                        Code = 0
                                    },
                                new TRole
                                    {
                                        Name = "Patient",
                                        Code = 1
                                    },
                                new TRole
                                    {
                                        Name = "Doctor",
                                        Code = 2
                                    },
                                new TRole
                                    {
                                        Name = "Admin",
                                        Code = 3
                                    }
                            };
        }

        #endregion
    }
}