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

namespace SmartHome.Pages.ShopItems
{
    public class DeleteModel : PageModel
    {
        private readonly IShopItemData shopItemData;

        public DeleteModel(IShopItemData shopItemData)
        {
            this.shopItemData = shopItemData;
        }

        [BindProperty]
        public ShopItem ShopItem { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ShopItem = shopItemData.GetShopItemById(id.Value);

            if (ShopItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost(int? id)
        {
            if (id.Value == 0)
            {
                return RedirectToPage("NotFound");
            }

            ShopItem = shopItemData.GetShopItemById(id.Value);

            if (ShopItem != null)
            {
                shopItemData.Delete(ShopItem.Id);
            }
            else
            {
                return RedirectToPage("NotFound");
            }
            shopItemData.Commit();
            return RedirectToPage("./Index");
        }
    }
}
