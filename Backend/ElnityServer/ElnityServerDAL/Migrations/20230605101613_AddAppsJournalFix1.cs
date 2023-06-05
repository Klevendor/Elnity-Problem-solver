using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElnityServerDAL.Migrations
{
    public partial class AddAppsJournalFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InDevelop",
                table: "Apps",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InDevelop",
                table: "Apps");
        }
    }
}
