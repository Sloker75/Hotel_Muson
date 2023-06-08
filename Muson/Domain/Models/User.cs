using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Comments = new List<Comment>();
            ExtraServices = new List<ExtraService>();
            Bookings = new List<Booking>();
        }
        public string Name { get; set; }
        public string Surname { get; set; }

        // public string Photo { get; set; }
        // public DateTime DateOfBirth { get; set;}

        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<ExtraService>? ExtraServices { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
