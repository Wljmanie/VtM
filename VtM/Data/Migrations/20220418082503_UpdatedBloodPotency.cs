using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class UpdatedBloodPotency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisciplineRouseCheckReroll",
                table: "BloodPotencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisciplineRouseCheckReroll",
                table: "BloodPotencies");
        }
    }
}
