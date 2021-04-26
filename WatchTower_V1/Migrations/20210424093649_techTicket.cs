using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class techTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalTicket_AspNetUsers_StaffId",
                table: "TechnicalTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalTicket_Item_AssetsId",
                table: "TechnicalTicket");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalTicket_AssetsId",
                table: "TechnicalTicket");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalTicket_StaffId",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "AssetsId",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "StaffStudentId",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "TimeOpened",
                table: "TechnicalTicket");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "TechnicalTicket",
                newName: "AssetId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TechnicalTicket",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TechnicalTicket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isClosed",
                table: "TechnicalTicket",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_AssetId",
                table: "TechnicalTicket",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_UserId",
                table: "TechnicalTicket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalTicket_AspNetUsers_UserId",
                table: "TechnicalTicket",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalTicket_Item_AssetId",
                table: "TechnicalTicket",
                column: "AssetId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalTicket_AspNetUsers_UserId",
                table: "TechnicalTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalTicket_Item_AssetId",
                table: "TechnicalTicket");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalTicket_AssetId",
                table: "TechnicalTicket");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalTicket_UserId",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TechnicalTicket");

            migrationBuilder.DropColumn(
                name: "isClosed",
                table: "TechnicalTicket");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "TechnicalTicket",
                newName: "ItemId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TechnicalTicket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "AssetsId",
                table: "TechnicalTicket",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffId",
                table: "TechnicalTicket",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StaffStudentId",
                table: "TechnicalTicket",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOpened",
                table: "TechnicalTicket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_AssetsId",
                table: "TechnicalTicket",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalTicket_StaffId",
                table: "TechnicalTicket",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalTicket_AspNetUsers_StaffId",
                table: "TechnicalTicket",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalTicket_Item_AssetsId",
                table: "TechnicalTicket",
                column: "AssetsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
