using Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class ExtraService
    {
        [Key]
        public int Id { get; set; }
        public TypeExtraService TypeExtraService { get; set; }
        public int Price { get; set; }
        public DateTime ExecutionTime { get; set; }
        public DateTime CreationTime { get; set; }
        public int RoomNumber { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public User? User { get; set; }

        //TODO: Menu
    }
}
