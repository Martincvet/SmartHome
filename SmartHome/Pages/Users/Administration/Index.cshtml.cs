using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Core.AppUser;
using Core;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace SmartHome.Pages.Users.Administration
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<App_User> userManager;
        public IndexModel(UserManager<App_User> userManager)
        {
            this.userManager = userManager;
        }

        public IEnumerable<App_User> App_Users { get; set; }
        public void OnGet()
        {
            App_Users = userManager.Users.ToList();
        }
    }
}
