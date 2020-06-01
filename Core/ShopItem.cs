using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Core;

namespace Core
{
    public class ShopItem
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Required Name of Product"),MinLength(3,ErrorMessage = "Name of Product is Required")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Choose your ShopItem Type")]
        public ShopItemType ItemType { get; set; }
        [Required]
        [Display(Name = "Is this Avalible to the Market")]
        public bool Availability { get; set; }
        [DataType(DataType.ImageUrl)]
        public string PhotoPath { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
    }
}
