using Data.Interface;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.Pages.ViewComponents
{
    public class ShopItemsCountViewComponents : ViewComponent
    {
        private readonly IShopItemData shopItemData;
        public ShopItemsCountViewComponents(IShopItemData shopItemData)
        {
            this.shopItemData = shopItemData;
        }

        public IViewComponentResult Invoke()
        {
            var temporary = shopItemData.Count();
            return View(temporary);
        }
    }
}
