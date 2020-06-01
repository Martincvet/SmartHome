using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core;
using Data;
using Data.Interface;

namespace SmartHome.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly IMembershipData membershipData;

        public DeleteModel(IMembershipData membershipData)
        {
            this.membershipData = membershipData;
        }

        [BindProperty]
        public Membership Membership { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("NotFound");
            }

            Membership = membershipData.GetMembershipById(id.Value);

            if (Membership == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("NotFound");
            }

            if(Membership.Id == 0)
            {
                return RedirectToPage("./Edit");
            }

            if (Membership != null)
            {
                Membership = membershipData.DeleteMembership(Membership.Id);
            }
            membershipData.Commit();
            return RedirectToPage("./List");
        }
    }
}
