using Core.AppUser;
using System.Collections.Generic;

namespace Core
{
    public class Visit
    {
        public List<ShopItem> ShopItems;
        public Visit()
        {
            ShopItems = new List<ShopItem>();
        }
        // ovde kje se plakja za site shopitems, 
        //znachi kje ima kolku pari treba da plati, 
        //koja e normalna cena, koja e od popustot, kolku popust.

        public int Id { get; set; }
        public string UserId { get; set; }
        public App_User User { get; set; }
    }
}
