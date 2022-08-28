using Microsoft.EntityFrameworkCore.Migrations;

namespace FootballProgrammes.Data.Migrations
{
    public partial class updateEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Messages",
                newName: "isDeleted");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "MediaFiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "HomeClubs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "FootballProgrammes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AwayClubs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "MediaFiles");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "HomeClubs");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "FootballProgrammes");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AwayClubs");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Messages",
                newName: "IsDeleted");
        }
    }
}
