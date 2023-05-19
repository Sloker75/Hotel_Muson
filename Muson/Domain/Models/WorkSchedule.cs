using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class WorkSchedule
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
