using Microsoft.EntityFrameworkCore.Migrations;

namespace CypherBot.Core.DataAccess.Migrations
{
    public partial class AddedOddities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Oddities",
                columns: table => new
                {
                    OddityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OddityDescription = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oddities", x => x.OddityId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oddities");
        }
    }
}
