using Microsoft.AspNetCore.Identity;

namespace Library.Entity
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
