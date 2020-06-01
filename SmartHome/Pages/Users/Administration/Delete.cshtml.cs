using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartHome.Pages.Users.Administration
{
    public class DeleteModel : PageModel
    {
        private readonly UserManager<App_User> userManager;
        public DeleteModel(UserManager<App_User> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public App_User App_User { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            App_User = await userManager.FindByIdAsync(id);

            if (App_User == null)
            {
                return RedirectToPage("NotFound");
            }
                  
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            App_User = await userManager.FindByIdAsync(id);

            if (App_User != null)
            {
                IdentityResult result = await userManager.DeleteAsync(App_User);

                if (result.Succeeded)
                    return RedirectToPage("./Index");
                else
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
            }
            else
            {
                ModelState.AddModelError("", "This user can't be found");
            }

            return RedirectToPage("./Index", userManager.Users);
        }
    }
}
