using Microsoft.AspNetCore.Identity;

namespace University_test_system.Models
{
    public class User : IdentityUser
    {
        public DateTime RegisteredAt { get; set; }
        public List<Attempt> Attempts { get; set; }
    }
}
