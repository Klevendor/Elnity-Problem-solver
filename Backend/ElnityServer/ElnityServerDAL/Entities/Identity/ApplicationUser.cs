using ElnityServerDAL.Entities.Security;
using Microsoft.AspNetCore.Identity;

namespace ElnityServerDAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public DateTime? Birthday { get; set; }

        public string? MyNumber { get; set; }

        public string? AvatarPath { get; set; }

        public string? FullName { get; set; }

        public string? BaseRoot { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
