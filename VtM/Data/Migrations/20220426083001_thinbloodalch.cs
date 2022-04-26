using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtM.Data.Migrations
{
    public partial class thinbloodalch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AthanorCorporis",
                table: "ThinBloodAlchemies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Calcinatio",
                table: "ThinBloodAlchemies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "ThinBloodAlchemies",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AthanorCorporis",
                table: "ThinBloodAlchemies");

            migrationBuilder.DropColumn(
                name: "Calcinatio",
                table: "ThinBloodAlchemies");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ThinBloodAlchemies");
        }
    }
}
