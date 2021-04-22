using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralTickets_AspNetUsers_StaffId",
                table: "GeneralTickets");

            migrationBuilder.DropIndex(
                name: "IX_GeneralTickets_StaffId",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "GeneralTickets");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GeneralTickets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralTickets_UserId",
                table: "GeneralTickets",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralTickets_AspNetUsers_UserId",
                table: "GeneralTickets",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralTickets_AspNetUsers_UserId",
                table: "GeneralTickets");

            migrationBuilder.DropIndex(
                name: "IX_GeneralTickets_UserId",
                table: "GeneralTickets");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GeneralTickets",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "GeneralTickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralTickets_StaffId",
                table: "GeneralTickets",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralTickets_AspNetUsers_StaffId",
                table: "GeneralTickets",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
