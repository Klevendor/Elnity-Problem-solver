using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.Account.Request
{
    public class ChangeUserInfoRequest
    {
        public string Email { get; set; }

        public string? Username { get; set; }

        public string? FullName { get; set; }

        public string? Number { get; set; }

        public DateTime? Birthday { get; set; }

        [JsonPropertyName("image")]
        public IFormFile? Image { get; set; }
    }
}
