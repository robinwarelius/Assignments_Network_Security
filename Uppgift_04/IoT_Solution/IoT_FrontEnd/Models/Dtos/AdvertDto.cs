using System.ComponentModel.DataAnnotations;

namespace IoT_FrontEnd.Models.Dtos
{
    public class AdvertDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
