using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public bool hasFridge { get; set; }
        public bool hasTV { get; set; }
        public bool hasInternet { get; set; }
        public bool hasSmoking { get; set; }
        public bool hasAirConditioning { get; set; }
        public bool hasParkingLot { get; set; }
        public bool hasIron { get; set; }
        public bool hasBabysitter { get; set; }
    }
}
