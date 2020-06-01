using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;

namespace SmartHome.Memberships
{
    [Authorize(Roles = "Manager")]
    [Authorize(Roles = "Administrator")]

    public class ListModel : PageModel
    {
        private readonly IMembershipData membershipData;
        public ListModel(IMembershipData membershipData)
        {
            this.membershipData = membershipData;
        }
        public string Search { get; set; }
        public IEnumerable<Membership> Memberships { get;set; }

        public IActionResult OnGet()
        {
            Memberships = membershipData.GetMemberships(Search);
            if(Memberships == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
    }
}
