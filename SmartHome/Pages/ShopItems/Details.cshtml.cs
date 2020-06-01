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

namespace SmartHome
{
    public class DetailsModel : PageModel
    {
        private readonly IShopItemData shopItemData;

        public DetailsModel(IShopItemData shopItemData)
        {
            this.shopItemData = shopItemData;
        }

        public ShopItem ShopItem { get; set; }

        public IActionResult OnGet(int? id)
        {
            ShopItem = shopItemData.GetShopItemById(id.Value);

            if(ShopItem == null)
            {
                return RedirectToPage("NotFound");
            }

            return Page();
        }        
    }
}
