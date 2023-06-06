using ElnityServerDAL.Entities.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElnityServerDAL.Configuration
{
    public class NoteAppConfig: IEntityTypeConfiguration<NoteApp>
    {
        public void Configure(EntityTypeBuilder<NoteApp> builder)
        {
            builder.HasOne(p => p.User).WithMany(p => p.NoteAppsFilelds).HasForeignKey(p => p.UserId);
        }
    }
}
