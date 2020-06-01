using System.Collections.Generic;
using System.Linq;
using Business;
using Core;
using Core.AppUser;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartHome.Pages.Shop
{
    [AllowAnonymous]
    public class ShopListModel : PageModel
    {
        private readonly IShopItemData shopItemData;
        private readonly IVisitRepository visitRepository;
        private readonly UserManager<App_User> userManager;
        public ShopListModel(IShopItemData shopItemData, IVisitRepository visitRepository, UserManager<App_User> userManager)
        {
            this.visitRepository = visitRepository;
            this.shopItemData = shopItemData;
            this.userManager = userManager;
        }

        [TempData]
        public string Message { get; set; }
        [BindProperty]
        public Core.Visit Visit { get; set; }
        public Calc Calc { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<ShopItem> Products { get; set; }
        public IEnumerable<ShopItem> Services { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchItem { get; set; }

        public IActionResult OnGet()
        {
            Products = shopItemData.Products(SearchItem);
            Services = shopItemData.Services(SearchItem);

            Visit = new Core.Visit();

            if (Visit == null)
            {
                return RedirectToPage("/NotFound");
            }

            var list = userManager.Users.ToList().Select(x=> new { Id = x.Id, Name = $"{x.FirstName} {x.LastName}"});
            Users = new SelectList(list, "Id", "Name");

            return Page();
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                Visit = visitRepository.Create(Visit);

                visitRepository.Commit();
                return RedirectToPage("./Buy");
            }

            Message = Visit.UserId == null ? "No user selected." : $"Total expences: {Calc.TotalPerVisit(Visit)}";
            var list = userManager.Users.ToList().Select(x => new { Id = x.Id, Name = $"{x.FirstName} {x.LastName}" });
            Users = new SelectList(list, "Id", "Name");

            return Page();
        }
    }
}

