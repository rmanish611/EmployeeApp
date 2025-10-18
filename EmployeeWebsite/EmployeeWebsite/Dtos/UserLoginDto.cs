using System.ComponentModel.DataAnnotations;

namespace EmployeeWebsite.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
