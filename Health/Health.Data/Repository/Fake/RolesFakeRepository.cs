using System.Collections.Generic;
using System.Linq;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.Data.Entities;

namespace Health.Data.Repository.Fake
{
    public sealed class RolesFakeRepository : CoreFakeRepository<IRole>, IRoleRepository
    {
        public RolesFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            _entities = new List<IRole>
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

        #region IRoleRepository<IRole> Members

        public IRole GetByName(string name)
        {
            return _entities.Where(x => x.Name == name).FirstOrDefault();
        }

        #endregion
    }
}