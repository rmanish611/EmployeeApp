namespace Employee_Management.Models
{
    public class EmployeeHobby
    {
        public int EmployeeId { get; set; }
        public EmployeeDetails Employee { get; set; }
        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }
    }
}
