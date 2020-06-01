using Core.AppUser;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Employee : App_User
    {
        public DateTime HireDate { get; set; }
        public string PhotoPath { get; set; }
        public int? RoleId { get; set; }
        public App_Role Role { get; set; }
        [Required]
        [Display(Name = "Enter from when do you start working.")]
        [DataType(DataType.Time)]
        public DateTime BeginingTime { get; set; }
        [Required]
        [Display(Name = "Enter time when you end working.")]
        [DataType(DataType.Time)]
        public DateTime EndingTime { get; set; }
        public bool IsRoleEmployee
        {
            get
            {
                return !Role.RoleName.Contains("Customer");
            }
        }
    }
}
