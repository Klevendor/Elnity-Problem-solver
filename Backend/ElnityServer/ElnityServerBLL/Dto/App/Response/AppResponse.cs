using Microsoft.AspNetCore.Http;

namespace ElnityServerBLL.Dto.App.Response
{
    public class AppResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public bool InDevelop { get; set; }
    }
}
