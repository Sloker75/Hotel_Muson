using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muson.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ExtraService>? ExtraServices { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
