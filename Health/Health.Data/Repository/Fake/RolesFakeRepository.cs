using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Data.Repository.Fake
{
    public sealed class RolesFakeRepository : CoreFakeRepository<Role>, IRoleRepository
    {
        public RolesFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            _entities = new List<Role>
                            {
                                new Role
                                    {
                                        Id = 1,
                                        Name = "Guest"
                                    },
                                new Role
                                    {
                                        Id = 2,
                                        Name = "Patient"
                                    },
                                new Role
                                    {
                                        Id = 3,
                                        Name = "Doctor"
                                    },
                                new Role
                                    {
                                        Id = 4,
                                        Name = "Admin"
                                    }
                            };
        }

        #region IRoleRepository Members

        public Role GetByName(string name)
        {
            return _entities.Where(x => x.Name == name).FirstOrDefault();
        }

        public Role GetById(int role_id)
        {
            return _entities.Where(x => x.Id == role_id).FirstOrDefault();
        }

        #endregion
    }
}