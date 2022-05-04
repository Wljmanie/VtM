using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class Randomfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Clans_ClanId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Havens_HavenId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "HavenId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClanId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Clans_ClanId",
                table: "Characters",
                column: "ClanId",
                principalTable: "Clans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Havens_HavenId",
                table: "Characters",
                column: "HavenId",
                principalTable: "Havens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Clans_ClanId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Havens_HavenId",
                table: "Characters");

            migrationBuilder.AlterColumn<int>(
                name: "HavenId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClanId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Clans_ClanId",
                table: "Characters",
                column: "ClanId",
                principalTable: "Clans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Havens_HavenId",
                table: "Characters",
                column: "HavenId",
                principalTable: "Havens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
