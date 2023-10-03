using Microsoft.AspNetCore.Identity;

namespace IoT_BackEnd.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
