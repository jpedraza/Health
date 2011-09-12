using Health.Core.Entities.POCO;

namespace Health.Core.Entities
{
    public class DefaultRoles
    {
        private Role _all;

        private Role _guest;

        public Role All
        {
            get
            {
                return _all ?? (_all = new Role
                                           {
                                               Name = "All"
                                           });
            }
            set { _all = value; }
        }

        public Role Guest
        {
            get
            {
                return _guest ?? (_guest = new Role
                                               {
                                                   Name = "Guest"
                                               });
            }
            set { _guest = value; }
        }
    }
}