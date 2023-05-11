using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Room
    {

        [Key]
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int CountRoom { get; set; }
        public int Floor { get; set; }
        public TypeRoom TypeRoom { get; set; }
        public RoomStatus Status { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
