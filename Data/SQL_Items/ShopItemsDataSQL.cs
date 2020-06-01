using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.SQL_Items
{
    public class ShopItemsDataSQL : IShopItemData
    {
        private readonly SmartHomeDbContext context;
        public ShopItemsDataSQL(SmartHomeDbContext context)
        {
            this.context = context;
        }
        public int Commit()
        {
            return context.SaveChanges();
        }

        public int Count()
        {
            return context.ShopItems.Count();
        }

        public ShopItem Create(ShopItem shopItem)
        {
            context.ShopItems.Add(shopItem);
            return shopItem;
        }

        public ShopItem Delete(int shopitemid)
        {
            ShopItem temp = context.ShopItems.SingleOrDefault(s => s.Id == shopitemid);
            if(temp != null)
            {
                context.ShopItems.Remove(temp);
            }
            context.SaveChanges();

            return temp;
        }

        public IEnumerable<ShopItem> GetShopItems(string name = null)
        {
            var temp = !string.IsNullOrEmpty(name) ? $"{name}%" : name;
            return context.ShopItems.Where(s=>string.IsNullOrEmpty(name) || EF.Functions.Like(s.Name,temp)).ToList();
        }

        public ShopItem GetShopItemById(int shopitemid)
        {
            return context.ShopItems.SingleOrDefault(s=>s.Id == shopitemid);
        }

        public ShopItem Update(ShopItem shopItem)
        {
            context.Entry(shopItem).State = EntityState.Modified;
            return shopItem;
            
        }

        public IEnumerable<ShopItem> Products(string name = null)
        {
            var temp = !string.IsNullOrEmpty(name) ? $"{name}%" : name;
            return context.ShopItems
                .Include(x => x.ItemType)
                .Where(x => x.ItemType == ShopItemType.Product)
                .Where(s => string.IsNullOrEmpty(name) || EF.Functions.Like(s.Name, temp))
                .ToList();
        }

        public IEnumerable<ShopItem> Services(string name = null)
        { 
            var temp = !string.IsNullOrEmpty(name) ? $"{name}%" : name;
            return context.ShopItems
                .Include(x => x.ItemType)
                .Where(x => x.ItemType == ShopItemType.Service)
                .Where(s => string.IsNullOrEmpty(name) || EF.Functions.Like(s.Name, temp))
                .ToList();
        }
    }
}
