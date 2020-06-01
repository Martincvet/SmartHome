using Core;
using Core.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SmartHome.Pages.UserRoles
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<App_Role> roleManager;
        public CreateRoleModel(RoleManager<App_Role> roleManager)
        {
            this.roleManager = roleManager;
        }
        [BindProperty]
        public App_Role IdentityRole { get; set; }
        [TempData]
        public string Message { get; set; }
        public IActionResult OnGet()
        {
            App_Role app_Role = new App_Role();

            if(app_Role.Id != null)
            {
                return RedirectToPage("./EditRole");
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(App_Role userRole)
        {
            if (ModelState.IsValid)
            {
                App_Role temp = new App_Role
                {
                    Id = userRole.Id,
                    Description = userRole.Description,
                    RoleName = userRole.RoleName,
                };

                var result = await roleManager.CreateAsync(temp);
                TempData["Message"] = "New Role has been Created!";

                if (result.Succeeded)
                {
                    return RedirectToPage("./List");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return Page();
        }
    }
}
