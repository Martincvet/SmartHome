using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(string employeeid);
        Employee Create(Employee employee);
        Employee Update(Employee employee);
        Employee RemoveEmployee(string employeeid);
        int Commit();

    }
}
