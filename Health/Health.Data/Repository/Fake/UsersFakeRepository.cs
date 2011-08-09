using System;
using System.Collections.Generic;
using System.Linq;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Health.Data.Entities;

namespace Health.Data.Repository.Fake
{
    public sealed class UsersFakeRepository : CoreFakeRepository<IUser>, IUserRepository
    {
        public UsersFakeRepository(IDIKernel di_kernel, ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            Save(new User
                     {
                         FirstName = "Анатолий",
                         LastName = "Петров",
                         ThirdName = "Витальевич",
                         Login = "admin",
                         Password = "admin",
                         Role = CoreKernel.RoleRepo.GetByName("Admin")
                     });
            Save(new User
                     {
                         FirstName = "Максим",
                         LastName = "Васильев",
                         ThirdName = "Александрович",
                         Login = "patient",
                         Password = "patient",
                         Role = CoreKernel.RoleRepo.GetByName("Patient")
                     });
        }

        #region IUserRepository<IUser> Members

        public IUser GetByLoginAndPassword(string login, string password)
        {
            User required_user = default(User);
            IEnumerable<IUser> found_user = (from user in _entities
                                             where user.Login == login &
                                                   user.Password == password
                                             select user).ToList();

            if (found_user.Count() == 1)
            {
                required_user = found_user.First() as User;
            }
            return required_user;
        }

        public IUser GetByLogin(string login)
        {
            User required_user = default(User);
            IEnumerable<IUser> found_user = (from user in _entities
                                             where user.Login == login
                                             select user).ToList();

            if (found_user.Count() == 1)
            {
                required_user = found_user.First() as User;
            }
            return required_user;
        }

        #endregion
    }
}