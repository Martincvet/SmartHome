using System.Threading.Tasks;
using Core.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHome.Pages.UserRoles
{
    public class EditRoleModel : PageModel
    {
        private readonly RoleManager<App_Role> roleManagment;
        public EditRoleModel(RoleManager<App_Role> roleManager)
        {
            roleManagment = roleManager;
        }
        public App_Role App_Role { get; set; }
        [TempData]
        public string Message { get; set; } // Eventualno sakav nesho da ispishe deka go kreiral koga kje se vrakja vo index
        public async Task<IActionResult> OnGet(string id)
        {
            App_Role = await roleManagment.FindByIdAsync(id);

            if (App_Role == null)
            {
                return RedirectToPage("~/NotFound");
            }
 
            return Page();
        }

        public async Task<IActionResult> OnPost(App_Role userRole)
        {
            if (ModelState.IsValid)
            {
                App_Role = await roleManagment.FindByIdAsync(userRole.Id);

                if(App_Role == null)
                {
                    return RedirectToPage("~/NotFound");
                }
                else
                {
                    App_Role.RoleName = userRole.RoleName;
                    App_Role.Description = userRole.Description;
                    App_Role.Id = userRole.Id;

                    IdentityResult drugo = await roleManagment.UpdateAsync(App_Role);

                    if (drugo.Succeeded)
                    {
                        return RedirectToPage("./Index");
                    }

                    foreach (IdentityError error in drugo.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToPage("./List");
        }
    }
}
