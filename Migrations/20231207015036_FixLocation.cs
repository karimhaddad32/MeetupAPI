using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 13, 20, 50, 36, 16, DateTimeKind.Local).AddTicks(689));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 20, 20, 50, 36, 18, DateTimeKind.Local).AddTicks(463));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2023, 12, 13, 20, 45, 4, 974, DateTimeKind.Local).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2023, 12, 20, 20, 45, 4, 976, DateTimeKind.Local).AddTicks(9718));
        }
    }
}
