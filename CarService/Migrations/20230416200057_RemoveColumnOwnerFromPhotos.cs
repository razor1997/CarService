using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class RemoveColumnOwnerFromPhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Photos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Owner",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
