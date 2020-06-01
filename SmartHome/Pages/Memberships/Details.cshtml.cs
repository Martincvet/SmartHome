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
    public class DetailsModel : PageModel
    {
        private readonly IMembershipData membershipData;

        public DetailsModel(IMembershipData membershipData)
        {
            this.membershipData = membershipData;
        }

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
    }
}
