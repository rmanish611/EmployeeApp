using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Salary { get; set; }
        [Required]
        public string? Contact { get; set; }
        [Required]
        public string? ResumePath { get; set; }
        [Required]
        public string? ImagePath { get; set; }
        public bool IsWorking { get; set; }
        public bool IsAgree { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<EmployeeHobby> EmployeeHobbies { get; set; }
    }
}
