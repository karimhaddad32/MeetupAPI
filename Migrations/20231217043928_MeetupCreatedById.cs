using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupAPI.Migrations
{
    /// <inheritdoc />
    public partial class MeetupCreatedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Meetups",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedById", "Date" },
                values: new object[] { null, new DateTime(2023, 12, 23, 23, 39, 26, 557, DateTimeKind.Local).AddTicks(9151) });

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedById", "Date" },
                values: new object[] { null, new DateTime(2023, 12, 30, 23, 39, 26, 557, DateTimeKind.Local).AddTicks(9648) });

            migrationBuilder.CreateIndex(
                name: "IX_Meetups_CreatedById",
                table: "Meetups",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetups_Users_CreatedById",
                table: "Meetups",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meetups_Users_CreatedById",
                table: "Meetups");

            migrationBuilder.DropIndex(
                name: "IX_Meetups_CreatedById",
                table: "Meetups");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Meetups");

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 17, 22, 0, 35, 146, DateTimeKind.Local).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 24, 22, 0, 35, 146, DateTimeKind.Local).AddTicks(8249));
        }
    }
}
