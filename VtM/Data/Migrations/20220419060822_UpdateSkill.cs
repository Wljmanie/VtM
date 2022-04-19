using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class UpdateSkill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillType",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillType",
                table: "Skills");
        }
    }
}
