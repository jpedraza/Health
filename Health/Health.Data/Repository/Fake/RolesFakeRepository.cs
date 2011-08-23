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
                                        Name = "Guest",
                                        Code = 0
                                    },
                                new Role
                                    {
                                        Name = "Patient",
                                        Code = 1
                                    },
                                new Role
                                    {
                                        Name = "Doctor",
                                        Code = 2
                                    },
                                new Role
                                    {
                                        Name = "Admin",
                                        Code = 3
                                    }
                            };
        }

        #region IRoleRepository Members

        public Role GetByName(string name)
        {
            return _entities.Where(x => x.Name == name).FirstOrDefault();
        }

        #endregion
    }
}