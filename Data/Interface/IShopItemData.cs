using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IShopItemData
    {
        IEnumerable<ShopItem> GetShopItems(string name = null);
        IEnumerable<ShopItem> Products(string name = null);
        IEnumerable<ShopItem> Services(string name = null);
        ShopItem GetShopItemById(int shopitemid);
        ShopItem Create(ShopItem shopItem);
        ShopItem Delete(int shopitemid);
        ShopItem Update(ShopItem shopItem);
        int Commit();
        int Count();
    }
}
