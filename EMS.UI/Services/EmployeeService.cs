using EMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Caching;
using EMS.Common.Interfaces;

namespace EMS.UI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRestApiCaller _restApiCaller;

        private readonly string _baseApiUrl = ConfigurationManager.AppSettings["EMSWebApiBaseUrl"];
        public EmployeeService(IRestApiCaller restApiCaller)
        {
            _restApiCaller = restApiCaller;
        }


        /// <summary>
        /// Add Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool AddEmployee(Employee employee)
        {
            var result = _restApiCaller.Post<Employee, string>(_baseApiUrl, "api/Employee/Add", employee);

            if(result == "Success") return true;
            return false;
        }

        /// <summary>
        /// Edit Employee data
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool EditEmployee(Employee employee)
        {
            var result = _restApiCaller.Put<Employee, string>(_baseApiUrl, "api/Employee/Update", employee);
            if (result == "Success") return true;
            return false;
            
        }

        /// <summary>
        /// Delete Employee by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                _restApiCaller.Delete(_baseApiUrl, "api/Employee/Delete/" + employeeId);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Get All Employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployees()
        {
            return _restApiCaller.Get<List<Employee>>(_baseApiUrl, "api/Employee");

        }

        /// <summary>
        /// Get Employee by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee GetEmployee(int employeeId)
        {
            return _restApiCaller.Get<Employee>(_baseApiUrl, "api/Employee/" + employeeId);
        }

        /// <summary>
        /// Get City List from Database or Cache
        /// </summary>
        /// <returns></returns>
        public IEnumerable<City> GetCities()
        {
            IEnumerable<City> cities = null;

            cities = MemoryCache.Default["Cached_Cities"] as IEnumerable<City>;

            if (cities == null)
            {
                CacheItemPolicy cip = new CacheItemPolicy()
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(60))
                };

                cities = _restApiCaller.Get<IEnumerable<City>>(_baseApiUrl, "api/City");

                MemoryCache.Default.Set("Cached_Cities" , cities, cip);
            }

            return cities;
        }

    }
}