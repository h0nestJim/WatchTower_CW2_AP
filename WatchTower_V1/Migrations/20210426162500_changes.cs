using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUpdates_GeneralTickets_GeneralTicketId",
                table: "GeneralUpdates");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUpdates_GeneralTicketId",
                table: "GeneralUpdates");

            migrationBuilder.DropColumn(
                name: "GeneralTicketId",
                table: "GeneralUpdates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralTicketId",
                table: "GeneralUpdates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUpdates_GeneralTicketId",
                table: "GeneralUpdates",
                column: "GeneralTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUpdates_GeneralTickets_GeneralTicketId",
                table: "GeneralUpdates",
                column: "GeneralTicketId",
                principalTable: "GeneralTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
