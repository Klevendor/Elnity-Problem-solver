using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElnityServerDAL.Migrations
{
    public partial class AddAppsJournalFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "NoteAppUserFields");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "NoteAppUserFields",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
