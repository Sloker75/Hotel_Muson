using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            ExtraServices = new List<ExtraService>();
            Bookings = new List<Booking>();
        }

        public string Name { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ExtraService>? ExtraServices { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
