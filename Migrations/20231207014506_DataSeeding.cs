using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "<Pending>")]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Meetups",
                columns: new[] { "Id", "Date", "IsPrivate", "Name", "Organizer" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 13, 20, 45, 4, 974, DateTimeKind.Local).AddTicks(5457), false, "Tech Meetup", "John Doe" },
                    { 2, new DateTime(2023, 12, 20, 20, 45, 4, 976, DateTimeKind.Local).AddTicks(9718), true, "Data Science Forum", "Alice Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "Author", "Description", "MeetupId", "Topic" },
                values: new object[,]
                {
                    { 1, "Jane Smith", "A beginner-friendly session on programming basics.", 1, "Introduction to Programming" },
                    { 2, "Bob Johnson", "Exploring the latest trends in web development.", 1, "Web Development Trends" },
                    { 3, "Charlie Brown", "An introduction to the fundamentals of machine learning.", 2, "Machine Learning Basics" },
                    { 4, "Diana Davis", "Analyzing large datasets for actionable insights.", 2, "Big Data Analytics" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "MeetupId", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Anytown", 1, "12345", "123 Main St" },
                    { 2, "Sciencetown", 2, "54321", "456 Data St" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meetups",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
