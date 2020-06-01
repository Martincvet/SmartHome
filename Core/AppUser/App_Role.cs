using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.AppUser
{
    public class App_Role : IdentityRole
    {
        public string RoleName { get; set; }
       
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        //[NotMapped]
        //public List<string> Users { get; set; }


        // How to make other Identity fields also required
    }
}
