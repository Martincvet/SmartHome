using Core;
using Data.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SmartHome.Pages.Users.Employees
{
    [Authorize(Roles = "Administrator")]
    public class ListEmployeeModel : PageModel
    {
        private readonly IEmployeeRepository employeeRepository;
        public ListEmployeeModel(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> Employees { get; set; }
        public void OnGet()
        {
            Employees = employeeRepository.GetEmployees();
        }
    }
}