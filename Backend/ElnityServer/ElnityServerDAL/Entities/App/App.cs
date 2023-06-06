namespace ElnityServerDAL.Entities.App
{
    public class App
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public bool InDevelop { get; set; }

        public List<JournalApp> Apps { get; set; }
    }
}
