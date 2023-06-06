namespace ElnityServerBLL.Dto.App.Request
{
    public class ChangeAppStatusRequest
    {
        public Guid AppId { get; set; }

        public bool NewDevelopingStatus { get; set; }
    }
}
