using Health.API.Entities;

namespace Health.Data.Entities
{
    public class DefaultRoles : IDefaultRoles
    {
        private IRole _all;

        private IRole _guest;

        #region IDefaultRoles Members

        public IRole All
        {
            get
            {
                return _all ?? (_all = new Role
                                           {
                                               Name = "All",
                                               Code = 98
                                           });
            }
            set { _all = value; }
        }

        public IRole Guest
        {
            get
            {
                return _guest ?? (_guest = new Role
                                               {
                                                   Name = "Guest",
                                                   Code = 99
                                               });
            }
            set { _guest = value; }
        }

        #endregion
    }
}