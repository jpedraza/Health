using System.Collections.Generic;
using System.Linq;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.Entities;
using Health.Core.Entities.POCO;
using Health.Core.Entities.POCO.Abstract;

namespace Health.Data.Repository.Fake
{
    public sealed class UsersFakeRepository : CoreFakeRepository<User>, IUserRepository
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

        #region IUserRepository Members

        public User GetByLoginAndPassword(string login, string password)
        {
            User required_user = default(User);
            IEnumerable<User> found_user = (from user in _entities
                                            where user.Login == login &
                                                  user.Password == password
                                            select user).ToList();

            if (found_user.Count() == 1)
            {
                required_user = found_user.First();
            }
            return required_user;
        }

        public User GetById(int user_id)
        {
            return _entities.Where(e => e.Id == user_id).FirstOrDefault();
        }

        public User GetByLogin(string login)
        {
            User required_user = default(User);
            IEnumerable<User> found_user = (from user in _entities
                                            where user.Login == login
                                            select user).ToList();

            if (found_user.Count() == 1)
            {
                required_user = found_user.First();
            }
            return required_user;
        }

        #endregion
    }
}