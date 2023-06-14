using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Cars",
                newName: "YearProduction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearProduction",
                table: "Cars",
                newName: "MyProperty");
        }
    }
}
