using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHome.Pages.UserRoles
{
    public class ListModel : PageModel
    {
        private readonly RoleManager<App_Role> roleManager;
        public ListModel(RoleManager<App_Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IEnumerable<App_Role> IdentityRoles { get; set; }

        public void OnGet()
        {
            IdentityRoles = roleManager.Roles.ToList();
        }
    }
}
