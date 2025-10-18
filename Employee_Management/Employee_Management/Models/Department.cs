using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public ICollection<EmployeeDetails> Employees { get; set; }
    }
}
