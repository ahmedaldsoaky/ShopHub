using Microsoft.AspNetCore.Identity;

namespace myshop.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
