﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Employee
    {
        public Employee()
        {
            WorkSchedules = new List<WorkSchedule>();
        }
        public int Id { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<WorkSchedule> WorkSchedules { get; set; }
    }
}
