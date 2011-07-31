using System;
using System.Collections.Generic;
using System.Linq;
using Health.API;
using Health.API.Entities;
using Health.API.Repository;
using Ninject;

namespace Health.Data.Repository.Fake
{
    public sealed class UsersFakeRepository<TUser> : CoreFakeRepository<TUser, IUser>, IUserRepository<IUser>
        where TUser : class, IUser, new()
    {
        public IUser GetByLoginAndPassword(string login, string password)
        {
            TUser required_user = default(TUser);
            IEnumerable<IUser> found_user = from user in _entities
                                            where user.Login == login &
                                                  user.Password == password
                                            select user;

            if (found_user.Count() == 1)
            {
                required_user = found_user.First() as TUser;
            }
            return required_user;
        }

        public IUser GetByLogin(string login)
        {
            TUser required_user = default(TUser);
            IEnumerable<IUser> found_user = from user in _entities
                                            where user.Login == login
                                            select user;

            if (found_user.Count() == 1)
            {
                required_user = found_user.First() as TUser;
            }
            return required_user;
        }

        public override void InitializeData()
        {
            Save(new TUser
            {
                FirstName = "Анатолий",
                LastName = "Петров",
                ThirdName = "Витальевич",
                Login = "admin",
                Password = "admin",
                Role = CoreKernel.RoleRepo.GetByName("Admin")
            });
            Save(new TUser
            {
                FirstName = "Максим",
                LastName = "Васильев",
                ThirdName = "Александрович",
                Login = "patient",
                Password = "patient",
                Role = CoreKernel.RoleRepo.GetByName("Patient")
            });
        }
    }
}