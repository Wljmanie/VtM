using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class HavenImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileContentType",
                table: "Havens");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "Havens");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Havens");

            migrationBuilder.CreateTable(
                name: "HavenImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HavenId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HavenImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HavenImages_Havens_HavenId",
                        column: x => x.HavenId,
                        principalTable: "Havens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HavenImages_HavenId",
                table: "HavenImages",
                column: "HavenId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HavenImages");

            migrationBuilder.AddColumn<string>(
                name: "FileContentType",
                table: "Havens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileData",
                table: "Havens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Havens",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
