using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http; // Ye line add karna zaruri hai file upload ke liye

namespace Employee_Management.Models
{
    public class EmployeeDTO
    {
        // Ye fields form me input ke liye chahiye
        public int EmployeeId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string? Contact { get; set; }

        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int DepartmentId { get; set; }
        public int UserId { get; set; }

        // File upload properties: IFormFile ka use karein
        public IFormFile? ResumeFile { get; set; }
        public IFormFile? ImageFile { get; set; }

        // Dropdown Lists
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<State> States { get; set; } = new List<State>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Hobby> Hobbies { get; set; } = new List<Hobby>();

        // Selected Hobbies (form se aayege)
        public List<int> SelectedHobbies { get; set; } = new List<int>();

        // Ye fields "Other" options ke liye hai
        public string? OtherCountry { get; set; }
        public string? OtherState { get; set; }
        public string? OtherCity { get; set; }

        // Checkbox fields
        public bool IsAgree { get; set; }
        public bool IsWorking { get; set; }
    }
}