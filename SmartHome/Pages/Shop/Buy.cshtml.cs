using System.Linq;
using Business;
using Core;
using Core.AppUser;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHome.Pages.Shop
{
    [AllowAnonymous]
    public class BuyModel : PageModel
    {
        private readonly UserManager<App_User> userManager;
        private readonly Calc calc;
        private readonly IVisitRepository visitRepository;
        private readonly IShopItemData shopItemData;
        public BuyModel(UserManager<App_User> userManager, Calc calc, IVisitRepository visitRepository, IShopItemData shopItemData)
        {
            this.userManager = userManager;
            this.calc = calc;
            this.visitRepository = visitRepository;
            this.shopItemData = shopItemData;
        }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        [BindProperty]
        public Core.Visit Visit { get; set; }


        public IActionResult OnGet(int? visitid)
        {
            if(visitid == null)
            {
                return RedirectToPage("/NotFound");
            }

            Visit = visitRepository.GetVisitById(visitid.Value);
            Visit.User = userManager.Users.SingleOrDefault();

            return Page();
        }

        public IActionResult OnPost(int? visitid)
        {
            if (ModelState.IsValid)
            {
                if(visitid.HasValue)
                {
                    Visit = visitRepository.GetVisitById(visitid.Value);
                }

                Visit = visitRepository.Update(Visit);

                visitRepository.Commit();
                return RedirectToPage("./ShopList");
            }

            Message = Visit.UserId == null ? "No customer selected!" : $"Total pay: {calc.TotalPerVisit(Visit)}";
            return Page();
        }


        public double? MembershipPrice(ShopItem shopItem)
        {
            shopItem = shopItemData.GetShopItemById(shopItem.Id);

            var product = Visit.User.Membership.Discount_Product;
            var service = Visit.User.Membership.Discount_Service;

            if (shopItem.ItemType == ShopItemType.Product)
            {
                var nesho = shopItem.Price * product;
                return nesho;
            }
            
            if(shopItem.ItemType == ShopItemType.Service)
            {
                var nesho = shopItem.Price * service;
                return nesho;
            }

            return null;
        }

        public double TotalPerVisit(Core.Visit visit)
        {
            return calc.TotalPerVisit(visit);
        }

    }
}
