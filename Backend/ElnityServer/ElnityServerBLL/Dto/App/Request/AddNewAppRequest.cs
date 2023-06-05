using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace ElnityServerBLL.Dto.App.Request
{
    public class AddNewAppRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }

        public bool InDevelop { get; set; } 
    }
}
