using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApi.Database.Migrations
{
    public partial class ContactsSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "date_of_birth", "first_name", "surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1964, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keanu", "Reeves" },
                    { 2, new DateTime(1981, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger", "Federer" },
                    { 3, new DateTime(1971, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "Wahlberg" },
                    { 4, null, "Superman", null },
                    { 5, new DateTime(1955, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bill", "Gates" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 5);
        }
    }
}
