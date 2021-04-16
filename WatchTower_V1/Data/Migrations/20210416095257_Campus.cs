using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Data.Migrations
{
    public partial class Campus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "campus",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Campus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campus", x => x.Id);
                });

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
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Campus_CampusId",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Campus");

            migrationBuilder.DropIndex(
                name: "IX_Room_CampusId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "campus",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
