using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class UpdateCarPartClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_CarMarkets_CarMarketId",
                table: "CarParts");

            migrationBuilder.AlterColumn<int>(
                name: "CarMarketId",
                table: "CarParts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_CarMarkets_CarMarketId",
                table: "CarParts",
                column: "CarMarketId",
                principalTable: "CarMarkets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarParts_CarMarkets_CarMarketId",
                table: "CarParts");

            migrationBuilder.AlterColumn<int>(
                name: "CarMarketId",
                table: "CarParts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarParts_CarMarkets_CarMarketId",
                table: "CarParts",
                column: "CarMarketId",
                principalTable: "CarMarkets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
