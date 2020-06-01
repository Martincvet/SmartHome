using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core;
using Microsoft.AspNetCore.Identity;
using Core.AppUser;
using Data.Interface;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace SmartHome.Pages.Users.Administration
{
    public class EditModel : PageModel
    {
        private readonly UserManager<App_User> userManager;
        private readonly IMembershipData membershipData;

        public EditModel(UserManager<App_User> userManager, IMembershipData membershipData)
        {
            this.membershipData = membershipData;
            this.userManager = userManager;
        }
        [BindProperty]
        public App_User User { get; set; }
        public IEnumerable<SelectListItem> Memberships { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            if(id == null)
            {
                return RedirectToPage("NotFound");
            }

            if(User.Id != null)
            {
                User = await userManager.FindByIdAsync(id);
            }

            if (User == null)
            {
                return RedirectToPage("/NotFound");
            }

            var list = membershipData.GetMemberships().ToList().Select(x => new { Id = x.Id, MembershipName = $"{x.MemberhipTypeName}" });
            Memberships = new SelectList(list, "Id", "MembershipName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(App_User user)
        {
            if (ModelState.IsValid)
            {
                if (User.Id == null)
                {
                    return RedirectToPage("/Register");
                }
                else
                {
                    User.Id = user.Id;
                    User.UserName = user.UserName;
                    User.FirstName = user.FirstName;
                    User.LastName = user.LastName;
                    User.Email = user.Email;
                    User.City = user.City;

                    IdentityResult drugo = await userManager.UpdateAsync(User);
                    TempData["TempMessage"] = "User Informartion Updated!";

                    if (drugo.Succeeded)
                    {
                        return RedirectToPage("./Index", userManager.Users);
                    }
                    else
                    ModelState.AddModelError("", "User not updated, something went wrong.");
                }
            }

            var list = membershipData.GetMemberships().ToList().Select(x => new { Id = x.Id, MembershipName = $"{x.MemberhipTypeName}" });
            Memberships = new SelectList(list, "Id", "MembershipName");
            return Page();
        }
    }
}
