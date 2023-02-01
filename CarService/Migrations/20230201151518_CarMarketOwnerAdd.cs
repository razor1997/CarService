using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class CarMarketOwnerAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "CarMarkets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarMarkets_OwnerId",
                table: "CarMarkets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarMarkets_Users_OwnerId",
                table: "CarMarkets",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarMarkets_Users_OwnerId",
                table: "CarMarkets");

            migrationBuilder.DropIndex(
                name: "IX_CarMarkets_OwnerId",
                table: "CarMarkets");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "CarMarkets");
        }
    }
}
