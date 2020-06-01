using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;

namespace SmartHome.Pages.ShopItems
{
    [Authorize(Roles = "Administrator")]
    public class IndexModel : PageModel
    {
        private readonly IShopItemData shopItemdata;

        public IndexModel(IShopItemData shopItem)
        {
            this.shopItemdata = shopItem;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchItem { get; set; }
        public IEnumerable<ShopItem> ShopItems { get;set; }

        public void OnGet()
        {
            ShopItems = shopItemdata.GetShopItems(SearchItem);
        }
    }
}
