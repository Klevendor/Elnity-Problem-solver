using ElnityServerDAL.Entities.Identity;

namespace ElnityServerDAL.Entities.App
{
    public class NoteApp
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string? CurrentState { get; set; }
        
        public string? Note { get; set;}

        public string ImagePath { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
