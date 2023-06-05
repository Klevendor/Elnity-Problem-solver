using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ElnityServerDAL.Configuration;
using ElnityServerDAL.Entities.Identity;
using ElnityServerDAL.Entities.App;

namespace ElnityServerDAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public DbSet<App> Apps { get; set; }

        public DbSet<JournalApp> JournalUserApps { get; set; }

        public DbSet<NoteApp> NoteAppUserFields { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());

            modelBuilder.ApplyConfiguration(new AppConfig());

            modelBuilder.ApplyConfiguration(new JournalAppConfig());

            modelBuilder.ApplyConfiguration(new NoteAppConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
