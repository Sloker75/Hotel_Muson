namespace Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
