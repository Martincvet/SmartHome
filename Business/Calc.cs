using Core;
using Data.Interface;
using System;
using System.Linq;

namespace Business
{
    public class Calc
    {
        public Membership Membership { get; set; }
        public double PopustProduct() // 80 posto --> 0.80
        {
            var popust = Membership.Discount_Product / 100;
            return popust;
        }
        public double KolkuPopustProduct()
        {
            var kolkuPopust = Membership.Discount_Product - 100;
            var drugo = kolkuPopust / 100;
            return drugo;
        }

        public double PopustService()
        {
            var popustservice = Membership.Discount_Service / 100;
            return popustservice;
        }

        public double KolkuPopustServices()
        {
            var kolkupopustservice = Membership.Discount_Service - 100;
            var nesho = kolkupopustservice / 100;
            return nesho;
        }

        public ShopItem CreateProduct(double pay)
        {
            return new ShopItem
            {
                Price = pay,
                ItemType = ShopItemType.Product,
            };
        }

        public ShopItem CreateService(double pay)
        {
            return new ShopItem
            {
                Price = pay,
                ItemType = ShopItemType.Service,
            };
        }


        public double TotalPerVisit(Visit visit)
        {
            var temp = 0.0;
            foreach (var product in visit.ShopItems.Where(i => i.ItemType == ShopItemType.Product).ToList())
            {
                if (visit.User.MembershipId != null)
                {
                    temp += product.Price * (1 - visit.User.Membership.Discount_Product);
                }
                else
                {
                    temp += product.Price;
                }
            }

            foreach (var service in visit.ShopItems.Where(i => i.ItemType == ShopItemType.Service).ToList())
            {
                if (visit.User.MembershipId != null)
                {
                    temp += service.Price * (1 - visit.User.Membership.Discount_Service);
                }
                else
                {
                    temp += service.Price;
                }
            }

            return temp;
        }


    }
}
