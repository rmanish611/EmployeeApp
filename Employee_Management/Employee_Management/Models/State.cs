using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class State
    {
        public int StateId { get; set; }
        [Required]
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<City> Cities { get; set; }
    }
}
