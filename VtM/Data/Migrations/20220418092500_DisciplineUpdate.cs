using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class DisciplineUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Books_BookId",
                table: "Disciplines");

            migrationBuilder.DropIndex(
                name: "IX_Disciplines_BookId",
                table: "Disciplines");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Disciplines");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Disciplines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_BookId",
                table: "Disciplines",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Books_BookId",
                table: "Disciplines",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
