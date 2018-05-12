using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.UI.Models;
using EMS.Common.Models;

namespace EMS.UI.Services
{
    public interface IEmployeeService
    {
        bool AddEmployee(Employee employee);

        bool EditEmployee(Employee employee);

        bool DeleteEmployee(int employeeId);

        List<Employee> GetEmployees();

        Employee GetEmployee(int employeeId);

        IEnumerable<City> GetCities();


    }
}
