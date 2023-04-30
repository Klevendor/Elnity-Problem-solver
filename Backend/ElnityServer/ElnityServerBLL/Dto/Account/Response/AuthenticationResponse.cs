using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.Account.Response
{
    public class AuthenticationResponse
    {
        public string InfoMessages { get; set; }

        public bool IsAuthenticated { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IList<string> UserRoles { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
