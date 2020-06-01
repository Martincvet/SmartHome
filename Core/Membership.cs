using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Membership
    {
        public int Id { get; set; }
        [Required]
        public string MemberhipTypeName { get; set; }
        [Required]
        public double Discount_Product { get; set; }
        [Required]
        public double Discount_Service { get; set; }

        
    }
}
