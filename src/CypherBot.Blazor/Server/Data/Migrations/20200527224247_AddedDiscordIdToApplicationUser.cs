using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Blazor.Server.Data.Migrations
{
    public partial class AddedDiscordIdToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscordID",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscordID",
                table: "AspNetUsers");
        }
    }
}
