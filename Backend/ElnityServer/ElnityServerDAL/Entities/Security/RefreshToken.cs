using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ElnityServerDAL.Entities.Security
{
    [Owned]
    public class RefreshToken
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string CreatedByIp { get; set; }
        public string? RevokedByIp { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
