using System.ComponentModel.DataAnnotations;

namespace IoT_BackEnd.Models
{
    public class Advertising
    {
        [Key]
        public int AdvertisingId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
    }
}
