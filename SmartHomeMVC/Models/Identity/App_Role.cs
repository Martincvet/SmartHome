using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHomeMVC.Models.Identity
{
    public class App_Role : IdentityRole
    {
        [Required]
        public string RoleName { get; set; }
    }
}
