using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApi.Database.Migrations
{
    public partial class RemoveSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "contact_data",
                keyColumn: "id",
                keyValue: 12);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "contacts",
                columns: new[] { "id", "address_number", "city", "country", "date_of_birth", "first_name", "postcode", "street", "surname" },
                values: new object[,]
                {
                    { 1, "8106", "Schenectady", "New York, US", new DateTime(1964, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keanu", "12302", "Linda Ave.", "Reeves" },
                    { 2, "7201", "Easton", "Pennsylvania, US", new DateTime(1981, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roger", "18042", "N. Roehampton Ave.", "Federer" },
                    { 3, "57", "Bern", "Switzerland", new DateTime(1971, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "3027", "Erlenweg", "Wahlberg" },
                    { 4, "3357", "Schenectady", "New York, US", null, "Superman", "12303", "Golden Ridge Road", null },
                    { 5, "4597", "Fort Lauderdale", "Florida, US", new DateTime(1955, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bill", "33308", "Pointe Lane", "Gates" }
                });

            migrationBuilder.InsertData(
                table: "contact_data",
                columns: new[] { "id", "contact_data_status", "contact_data_type", "contact_data_value", "contact_id", "created_or_updated" },
                values: new object[,]
                {
                    { 1, "Y", "PHONE", "0900000000", 1, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(1841) },
                    { 2, "Y", "PHONE", "0900000001", 1, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2537) },
                    { 3, "Y", "MAIL", "keanu.reeves@mail.com", 1, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2555) },
                    { 4, "Y", "PHONE", "0900000100", 2, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2560) },
                    { 5, "Y", "MAIL", "roger.federer@mail.com", 2, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2565) },
                    { 6, "Y", "MAIL", "roger.federer@anothermail.com", 2, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2569) },
                    { 7, "Y", "PHONE", "0900000200", 3, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2573) },
                    { 8, "Y", "MAIL", "mark.wahlberg@mail.com", 3, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2577) },
                    { 9, "Y", "MAIL", "superman@mail.com", 4, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2581) },
                    { 10, "Y", "PHONE", "0900000002", 4, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2585) },
                    { 11, "Y", "PHONE", "0901000100", 5, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2589) },
                    { 12, "Y", "MAIL", "bill.gates@mail.com", 5, new DateTime(2020, 12, 19, 21, 35, 49, 936, DateTimeKind.Local).AddTicks(2593) }
                });
        }
    }
}
