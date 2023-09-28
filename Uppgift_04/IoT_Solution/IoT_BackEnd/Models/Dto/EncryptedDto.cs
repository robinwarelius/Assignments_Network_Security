using System.ComponentModel.DataAnnotations;

namespace IoT_BackEnd.Models.Dto
{
    public class EncryptedDto
    {
        [Required]
        public byte[] Key { get; set; }
        [Required]
        public byte[] IV { get; set; }
        [Required]
        public byte[] SecretValue { get; set; }
    }
}
