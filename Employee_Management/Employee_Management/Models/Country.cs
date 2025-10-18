using System.ComponentModel.DataAnnotations;

namespace Employee_Management.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
        public ICollection<State> States { get; set; }
    }
}
