using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.Account.Response
{
    public class UserLoginResModel
    {
        public bool IsAuthenticated { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IList<string> UserRoles { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiration { get; set; }

        public string InfoMessages { get; set; }
    }
}
