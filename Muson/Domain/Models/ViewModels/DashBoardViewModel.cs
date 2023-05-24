using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ViewModels
{
    public class DashBoardViewModel
    {
        public DashBoardViewModel()
        {
            ExtraServices = new List<ExtraService>();
            Bookings = new List<Booking>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public ICollection<ExtraService>? ExtraServices { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
