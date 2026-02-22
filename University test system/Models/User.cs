using Microsoft.AspNetCore.Identity;

namespace University_test_system.Models
{
    public class User : IdentityUser
    {
        public int? FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
