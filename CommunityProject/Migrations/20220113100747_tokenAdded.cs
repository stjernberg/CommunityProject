using Microsoft.EntityFrameworkCore.Migrations;

namespace CommunityProject.Migrations
{
    public partial class tokenAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNr",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNr",
                table: "AspNetUsers");
        }
    }
}
