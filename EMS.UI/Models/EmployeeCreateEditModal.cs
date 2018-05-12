using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EMS.UI.Models
{
    public class EmployeeCreateEditModal
    {
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "Employee name is required.")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "City")]
        public string CityName { get; set; }
        
    }
}