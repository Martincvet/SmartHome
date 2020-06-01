using Core;
using Data.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartHome.Pages.Users.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMembershipData membershipData;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnvironment, IMembershipData membershipData)
        {
            this.employeeRepository = employeeRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.membershipData = membershipData;
        }

        [BindProperty]
        public IFormFile Photo { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }
        [TempData]
        public string Message { get; set; }
        public IEnumerable<SelectListItem> Memberships { get; set; }

        public IActionResult OnGet(string EmployeeId)
        {
            if(EmployeeId == null)
            {
                Employee = new Employee();
            }
            else
            {
                Employee = employeeRepository.GetEmployeeById(EmployeeId);
            }

            var list = membershipData.GetMemberships().ToList().Select(x => new { Id = x.Id, MembershipName = $"{x.MemberhipTypeName}" });
            Memberships = new SelectList(list, "Id", "MembershipName");
            return Page();
        }

        public IActionResult OnPost(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", employee.PhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = UploadingFilePhoto();
                }

                if (Employee.Id == null)
                {
                    Employee = employeeRepository.Create(Employee);
                    TempData["Message"] = "The Object is created!";
                }
                else
                {
                    Employee = employeeRepository.Update(Employee);
                    TempData["Message"] = "The Object is updated!";
                }

                employeeRepository.Commit();
                return RedirectToPage("./ListEmployee");
            }

            var list = membershipData.GetMemberships().ToList().Select(x => new { Id = x.Id, MembershipName = $"{x.MemberhipTypeName}" });
            Memberships = new SelectList(list, "Id", "MembershipName");
            return Page();
        }

        private string UploadingFilePhoto()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadSFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadSFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}