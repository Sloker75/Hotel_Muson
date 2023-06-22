using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ViewModels
{
    public class BookingViewModel
    {
        public int BookingId { get; set; }
        public DateTime DateArrival { get; set; }
        public DateTime DateDeparture { get; set; }
        public int Price { get; set; }
        public Room Room { get; set; }
        public Service? Service { get; set; }
        public User User { get; set; }
    }
}
