using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WatchTower_V1.Migrations
{
    public partial class updatetickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StaffStudentId",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "TimeOpened",
                table: "GeneralTickets");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "GeneralTickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isOpen",
                table: "GeneralTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "GeneralTickets");

            migrationBuilder.DropColumn(
                name: "isOpen",
                table: "GeneralTickets");

            migrationBuilder.AddColumn<string>(
                name: "StaffStudentId",
                table: "GeneralTickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOpened",
                table: "GeneralTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
