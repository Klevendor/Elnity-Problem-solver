using ElnityServerDAL.Entities.Identity;

namespace ElnityServerDAL.Entities.App
{
    public class JournalApp
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid AppId { get; set; }

        public ApplicationUser User { get; set; }

        public App App { get; set; }
    }
}
