using ElnityServerDAL.Entities.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElnityServerDAL.Configuration
{
    public class JournalAppConfig: IEntityTypeConfiguration<JournalApp>
    {
        public void Configure(EntityTypeBuilder<JournalApp> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.JournalApps).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.App).WithMany(p => p.Apps).HasForeignKey(p => p.AppId);

        }
    }
}
