using System.Threading.Tasks;
using Core.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHome.Pages.UserRoles
{
    public class DeleteRoleModel : PageModel
    {
        private readonly RoleManager<App_Role> roleManager;
        public DeleteRoleModel(RoleManager<App_Role> roleManager)
        {
            this.roleManager = roleManager;
        }
        public App_Role RoleIdentity { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            RoleIdentity = await roleManager.FindByIdAsync(id);
           
            if (RoleIdentity == null) 
            { 
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost(App_Role userRole)
        {
            if(ModelState.IsValid)
            {

                RoleIdentity = await roleManager.FindByIdAsync(userRole.Id);

                if(RoleIdentity == null)
                {
                    return RedirectToPage("~/NotFound");
                }

                IdentityResult delete = await roleManager.DeleteAsync(RoleIdentity);

                if(delete.Succeeded)
                {
                    return RedirectToPage("./Index");
                }

                foreach (IdentityError error in delete.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return RedirectToPage("~/NotFound");
        }
    }
}
