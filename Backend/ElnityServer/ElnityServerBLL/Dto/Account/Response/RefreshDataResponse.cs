using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.Account.Response
{
    public class RefreshDataResponse
    {
        [JsonIgnore]
        public string InfoMessages { get; set; }

        public string Email { get; set; }

        public IList<string> Roles { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
