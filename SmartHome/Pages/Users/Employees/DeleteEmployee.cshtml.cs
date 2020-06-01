using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHome.Pages.Users.Employees
{
    public class DeleteEmployeeModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        public DeleteEmployeeModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [BindProperty]
        public Employee Employee { get; set; }
        public IActionResult OnGet(string id)
        {
            if (id == null)
            {
                return RedirectToPage("/NotFound");
            }

            Employee = employeeRepository.GetEmployeeById(id);

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }

        public IActionResult OnPost()
        { 
            if (Employee != null)
            {
                Employee = employeeRepository.RemoveEmployee(Employee.Id);
            }
            else
            {
                ModelState.AddModelError("", "This user is Empty!");
            }

            employeeRepository.Commit();
            return RedirectToPage("./ListEmployee");
        }
    }
}