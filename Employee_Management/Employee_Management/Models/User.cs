using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAgree { get; set; }

        public EmployeeDetails EmployeeDetails { get; set; }
    }
}
