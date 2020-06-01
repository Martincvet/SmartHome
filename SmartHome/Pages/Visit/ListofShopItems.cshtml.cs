using Business;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SmartHome.Pages.Visit
{
    [AllowAnonymous]
    public class ListofShopItemsModel : PageModel
    {
        private readonly IVisitRepository visitRepository;
        private readonly Calc calculated;
        public ListofShopItemsModel(IVisitRepository visitRepository, Calc calculated)
        {
            this.visitRepository = visitRepository;
            this.calculated = calculated;
        }
        [TempData]
        public string Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public IEnumerable<Core.Visit> Visits { get; set; }
        public IActionResult OnGet()
        {
            Visits = visitRepository.GetVisits(Search);

            return Page();
        }

        public double TotalPerVisit(Core.Visit visit)
        {
            return calculated.TotalPerVisit(visit);
        }
    }
}
