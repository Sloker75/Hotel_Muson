using Muson.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Muson.Models
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
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
