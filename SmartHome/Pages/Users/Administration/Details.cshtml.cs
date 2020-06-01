using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Core.AppUser;

namespace SmartHome.Pages.Users.Administration
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<App_User> userManager;

        public DetailsModel(UserManager<App_User> userManager)
        {
            this.userManager = userManager;
        }

        public App_User App_User { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            App_User = await userManager.FindByIdAsync(id);

            if (App_User == null)
            {
                return RedirectToPage("NotFound");
            }
            
            return Page();
        }
    }
}
