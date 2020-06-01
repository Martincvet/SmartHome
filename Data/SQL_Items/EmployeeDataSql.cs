using Core;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.SQL_Items
{
    public class EmployeeDataSql : IEmployeeRepository
    {
        private readonly SmartHomeDbContext context;
        public EmployeeDataSql(SmartHomeDbContext context)
        {
            this.context = context;
        }

        public Employee Create(Employee employee)
        {
            context.Employees.Add(employee);
            return employee;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public Employee GetEmployeeById(string employeeid)
        {
            return context.Employees.SingleOrDefault(x => x.Id == employeeid);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public Employee RemoveEmployee(string employeeid)
        {
            var tempemployee = context.Employees.SingleOrDefault(x=>x.Id == employeeid);
            if(tempemployee != null)
            {
                context.Employees.Remove(tempemployee);
            }
            return tempemployee;
        }

        public Employee Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            return employee;
        }
    }
}
