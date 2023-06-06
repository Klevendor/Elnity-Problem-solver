using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.NoteAggregator.Request
{
    public class AddNoteRequest
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string? CurrentState { get; set; }

        public string? Note { get; set; }

        [JsonPropertyName("image")]
        public IFormFile Image { get; set; }
    }
}
