using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ViewModels
{
    public class RoomViewModel
    {
        public int RoomNumber { get; set; }
        public int CountRoom { get; set; }
        public int Floor { get; set; }
        public TypeRoom TypeRoom { get; set; }
        public RoomStatus Status { get; set; }
    }
}
