using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class AdjustedHaven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Publicity",
                table: "Havens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publicity",
                table: "Havens");
        }
    }
}
