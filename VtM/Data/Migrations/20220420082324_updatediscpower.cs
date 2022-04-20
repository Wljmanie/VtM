using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class updatediscpower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "DisciplinePowers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "System",
                table: "DisciplinePowers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "DisciplinePowers");

            migrationBuilder.DropColumn(
                name: "System",
                table: "DisciplinePowers");
        }
    }
}
