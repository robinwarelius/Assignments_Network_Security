using System.ComponentModel.DataAnnotations;

namespace IoT_FrontEnd.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
