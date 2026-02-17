using Microsoft.AspNetCore.Identity;

namespace University_test_system.Models
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime RegisteredAt { get; set; }
        public List<Attempt> Attempts { get; set; }
    }
}
