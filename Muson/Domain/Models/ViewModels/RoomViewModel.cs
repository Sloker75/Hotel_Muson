﻿using Domain.Enum;

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