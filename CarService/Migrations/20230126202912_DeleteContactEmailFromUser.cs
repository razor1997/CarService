using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class DeleteContactEmailFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
