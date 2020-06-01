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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Data.Interface;

namespace SmartHome.Pages.ShopItems
{
    public class EditModel : PageModel
    {
        private readonly IShopItemData shopdata;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHtmlHelper htmlHelper;
        public EditModel(IShopItemData shopdata,IHtmlHelper htmlHelper, IWebHostEnvironment webHostEnvironment)
        {
            this.shopdata = shopdata;
            this.htmlHelper = htmlHelper;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public bool Notification { get; set; }
        [TempData]
        public string Message { get; set; }

        [TempData]
        public string TempMessage { get; set; }

        public IEnumerable<SelectListItem> ShopItemType { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public ShopItem ShopItem { get; set; }

        public IActionResult OnGet(int? id)
        {
            if(id.HasValue)
            {
                ShopItem = shopdata.GetShopItemById(id.Value);
            }
            else
            {
                ShopItem = new ShopItem();
            }

            if (ShopItem == null)
            {
                return RedirectToPage("NotFound");
            }

            ShopItemType = htmlHelper.GetEnumSelectList<ShopItemType>();
            return Page();
        }

        public IActionResult OnPost(ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (ShopItem.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", shopItem.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    shopItem.PhotoPath = UploadingFilePhoto();
                }

                if(ShopItem.Id == 0)
                {
                    ShopItem = shopdata.Create(shopItem);
                    TempData["TempMessage"] = "You just created Shopping Product!";
                }
                else
                {
                    ShopItem = shopdata.Update(shopItem);
                    TempData["TempMessage"] = "You just updated Shopping Product!";
                }
                shopdata.Commit();
                return RedirectToPage("./Index", new { id = shopItem.Id});
            }

            ShopItemType = htmlHelper.GetEnumSelectList<ShopItemType>();
            return Page();

        }
        public RedirectToPageResult UpdateNotificationEmployees(int id)
        {
            if (Notification)
            {
                Message = "Employees will be notified for this product!";
            }
            else
            {
                Message = "Employees will be not notified for this product!";
            }
           
            ShopItem = shopdata.GetShopItemById(id);
            return RedirectToPage("Details", new { id = id });
        }

        private string UploadingFilePhoto()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadSFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadSFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
