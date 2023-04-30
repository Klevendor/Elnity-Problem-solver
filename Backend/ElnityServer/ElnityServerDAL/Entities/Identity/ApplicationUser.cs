using ElnityServerDAL.Entities.Security;
using Microsoft.AspNetCore.Identity;

namespace ElnityServerDAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
