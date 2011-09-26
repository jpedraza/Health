using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Health.Core.Entities.POCO;
using Health.Site.Models;

namespace Health.Site.Areas.Admin.Models
{
    public class UserList : CoreViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public Role Role { get; set; }

        public IEnumerable<Role> Roles { get; set; }

        public IEnumerable<SelectListItem> RolesSelectList
        {
            get
            {
                if (Roles == null) return new BindingList<SelectListItem>();
                var select_list_items = new BindingList<SelectListItem>();
                foreach (Role role in Roles)
                {
                    if (role.Name == "Guest") continue;
                    select_list_items.Add(new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.Name
                    });
                }
                return select_list_items;
            }
        }
    }
}