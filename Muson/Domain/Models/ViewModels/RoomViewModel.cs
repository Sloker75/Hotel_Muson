using Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.ViewModels
{
    public class RoomViewModel
    {
        public RoomViewModel()
        {
            Bookings = new List<Booking>();
        }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int CountRoom { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public TypeRoom TypeRoom { get; set; }
        [Required]
        public RoomStatus Status { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
