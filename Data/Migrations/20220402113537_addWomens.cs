using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballProgrammes.Data.Migrations
{
    public partial class addWomens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Womens",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Womens",
                table: "FootballProgrammes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Womens",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Womens",
                table: "FootballProgrammes");
        }
    }
}
