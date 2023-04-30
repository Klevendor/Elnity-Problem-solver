using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.Account.Response
{
    public class ShortAuthenticationResponse
    {
        public string InfoMessages { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }
    }
}
