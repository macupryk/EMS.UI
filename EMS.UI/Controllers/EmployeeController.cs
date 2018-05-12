using EMS.UI.Models;
using EMS.Common.Models;

using EMS.UI.Services;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace EMS.UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;


        public EmployeeController() { }

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employees = _employeeService.GetEmployees();
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = _employeeService.GetEmployee(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            //Get Cities
            var cityResult = _employeeService.GetCities().Select(c => new SelectListItem() { Text = c.CityName, Value = c.CityId.ToString() });

            EmployeeCreateEditModal employee = new EmployeeCreateEditModal();
            employee.Cities = cityResult;

            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeName, CityId")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _employeeService.AddEmployee(employee);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);

        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            //Get Employee
            var employeeResult = _employeeService.GetEmployee(id);

            //Get Cities
            var cityResult = _employeeService.GetCities().Select( c=> new SelectListItem() { Text = c.CityName , Value = c.CityId.ToString()});
            
            EmployeeCreateEditModal employee = new EmployeeCreateEditModal();
            employee.EmployeeId = employeeResult.EmployeeId;
            employee.EmployeeName = employeeResult.EmployeeName;
            employee.Cities = cityResult;
            employee.CityId = employeeResult.CityId;

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId, EmployeeName, CityId")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = _employeeService.EditEmployee(employee);
                    if (result)
                        return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = _employeeService.GetEmployee(id);

            if (employee == null) return HttpNotFound();

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool result = _employeeService.DeleteEmployee(id);

            if (result) return RedirectToAction("Index");

            return RedirectToAction("Delete");
        }
    }
}
