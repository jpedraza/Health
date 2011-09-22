using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models.Users
{
    public class UsersForm : CoreViewModel
    {
        public User User { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<SelectListItem> RolesSelectList 
        { 
            get
            {
                if (Roles == null) return new BindingList<SelectListItem>();
                var select_list_items = new BindingList<SelectListItem>();
                foreach (Role role in Roles)
                {
                    select_list_items.Add(new SelectListItem
                                              {
                                                  Selected = User == null || User.Role == null ? role.Name == "Patient" : User.Role.Id == role.Id,
                                                  Text = role.Name,
                                                  Value = role.Id.ToString()
                                              });
                }
                return select_list_items;
            } 
        }
    }
}