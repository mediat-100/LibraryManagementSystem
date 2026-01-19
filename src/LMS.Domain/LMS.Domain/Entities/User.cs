using LMS.Domain.Enums;

namespace LMS.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
