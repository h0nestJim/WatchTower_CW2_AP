using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Data.Migrations
{
    public partial class configroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Campus",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_CampusId",
                table: "Room",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Campus_CampusId",
                table: "Room",
                column: "CampusId",
                principalTable: "Campus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Campus_CampusId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_CampusId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Room");

            migrationBuilder.AddColumn<string>(
                name: "Campus",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
