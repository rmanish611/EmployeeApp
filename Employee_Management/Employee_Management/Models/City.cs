using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class City
    {
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public ICollection<EmployeeDetails> Employees { get; set; }
    }
}
