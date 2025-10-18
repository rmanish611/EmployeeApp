using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class Hobby
    {
        public int HobbyId { get; set; }
        [Required]
        public string HobbyName { get; set; }
        public ICollection<EmployeeHobby> EmployeeHobbies { get; set; }
    }
}
