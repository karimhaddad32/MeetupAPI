using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupAPI.Migrations
{
    /// <inheritdoc />
    public partial class MeetupCreatedByIdTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedById", "Date" },
                values: new object[] { 1, new DateTime(2023, 12, 24, 16, 11, 56, 185, DateTimeKind.Local).AddTicks(8630) });

            migrationBuilder.UpdateData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedById", "Date" },
                values: new object[] { 2, new DateTime(2023, 12, 31, 16, 11, 56, 186, DateTimeKind.Local).AddTicks(603) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
