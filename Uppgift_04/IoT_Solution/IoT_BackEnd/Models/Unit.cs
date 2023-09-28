using System.ComponentModel.DataAnnotations;

namespace IoT_BackEnd.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Temperature { get; set; }
    }
}
