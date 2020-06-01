using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core;
using Data;
using Data.Interface;

namespace SmartHome.Memberships
{
    public class EditModel : PageModel
    {
        private readonly IMembershipData membershipData;

        public EditModel(IMembershipData membershipData)
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

            if (Membership.Id == 0)
            {
                Membership = new Membership();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (MembershipExists(Membership.Id) == false)
                {
                    Membership = membershipData.CreateMembership(Membership);
                }
                else
                {
                    Membership = membershipData.Update(Membership);
                }

                membershipData.Commit();
                return RedirectToPage("./List");
            }

            return Page();
        }

        private bool MembershipExists(int id)
        {
            return membershipData.GetMemberships().Any(e => e.Id == id);
        }
    }
}
