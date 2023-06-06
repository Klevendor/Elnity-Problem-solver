using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.NoteAggregator.Response
{
    public class UserNoteResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string CurrentState { get; set; }

        public string Note { get; set; }
        
        public string ImagePath { get; set; }

    }
}
