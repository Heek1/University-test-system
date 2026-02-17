using Microsoft.AspNetCore.Identity;

namespace University_test_system.Models
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string Role { get; set; }
        public List<Attempt> Attempts { get; set; }
    }
}
