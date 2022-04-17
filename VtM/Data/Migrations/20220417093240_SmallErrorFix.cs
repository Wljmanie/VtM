using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class SmallErrorFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HavenImages_Havens_HavenId",
                table: "HavenImages");

            migrationBuilder.AlterColumn<int>(
                name: "HavenId",
                table: "HavenImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HavenImages_Havens_HavenId",
                table: "HavenImages",
                column: "HavenId",
                principalTable: "Havens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HavenImages_Havens_HavenId",
                table: "HavenImages");

            migrationBuilder.AlterColumn<int>(
                name: "HavenId",
                table: "HavenImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HavenImages_Havens_HavenId",
                table: "HavenImages",
                column: "HavenId",
                principalTable: "Havens",
                principalColumn: "Id");
        }
    }
}
