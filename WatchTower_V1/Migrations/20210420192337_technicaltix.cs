using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class technicaltix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralTickets_Item_AssetsId",
                table: "GeneralTickets");

            migrationBuilder.DropIndex(
                name: "IX_GeneralTickets_AssetsId",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "GeneralTickets");

            migrationBuilder.CreateTable(
                name: "TechnicalTicket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StaffStudentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    AssetsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalTicket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalTicket_AspNetUsers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalTicket_Item_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_AssetsId",
                table: "TechnicalTicket",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_StaffId",
                table: "TechnicalTicket",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalTicket");

            migrationBuilder.AddColumn<int>(
                name: "AssetsId",
                table: "GeneralTickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "GeneralTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "GeneralTickets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneralTickets_AssetsId",
                table: "GeneralTickets",
                column: "AssetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralTickets_Item_AssetsId",
                table: "GeneralTickets",
                column: "AssetsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
