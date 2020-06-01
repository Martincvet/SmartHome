using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.AppUser
{
    public class App_User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter the birth date")]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public int? MembershipId { get; set; }
        public Membership Membership { get; set; }
        public List<ShopItem> ShopItems { get; set; }
        //[Required]
        //[DataType(DataType.Password, ErrorMessage = "Please try again!")]
        //public string Password { get; set; }
    }
}
