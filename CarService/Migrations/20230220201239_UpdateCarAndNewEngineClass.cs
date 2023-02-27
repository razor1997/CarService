using Microsoft.EntityFrameworkCore.Migrations;

namespace CarService.Migrations
{
    public partial class UpdateCarAndNewEngineClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Cars",
                newName: "Body");

            migrationBuilder.AddColumn<int>(
                name: "EngineTypeId",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EngineTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngineTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_EngineTypeId",
                table: "Cars",
                column: "EngineTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_EngineTypes_EngineTypeId",
                table: "Cars",
                column: "EngineTypeId",
                principalTable: "EngineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_EngineTypes_EngineTypeId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "EngineTypes");

            migrationBuilder.DropIndex(
                name: "IX_Cars_EngineTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineTypeId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Cars",
                newName: "Type");
        }
    }
}
