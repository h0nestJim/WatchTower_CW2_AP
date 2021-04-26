using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class setkeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "GeneralUpdates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralUpdates_TicketId",
                table: "GeneralUpdates",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralUpdates_GeneralTickets_TicketId",
                table: "GeneralUpdates",
                column: "TicketId",
                principalTable: "GeneralTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralUpdates_GeneralTickets_TicketId",
                table: "GeneralUpdates");

            migrationBuilder.DropIndex(
                name: "IX_GeneralUpdates_TicketId",
                table: "GeneralUpdates");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "GeneralUpdates");
        }
    }
}
