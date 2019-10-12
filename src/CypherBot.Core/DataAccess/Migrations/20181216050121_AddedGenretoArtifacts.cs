using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedGenretoArtifacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Artifacts",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Artifacts");
        }
    }
}
