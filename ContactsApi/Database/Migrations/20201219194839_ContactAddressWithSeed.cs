using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApi.Database.Migrations
{
    public partial class ContactAddressWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "contact_uidx",
                table: "contacts");

            migrationBuilder.AddColumn<string>(
                name: "address_number",
                table: "contacts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "contacts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "contacts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "postcode",
                table: "contacts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "contacts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "address_number", "city", "country", "created_or_updated", "postcode", "street" },
                values: new object[] { "8106", "Schenectady", "New York, US", new DateTime(2020, 12, 19, 20, 48, 38, 671, DateTimeKind.Local).AddTicks(8786), "12302", "Linda Ave." });

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "address_number", "city", "country", "created_or_updated", "postcode", "street" },
                values: new object[] { "7201", "Easton", "Pennsylvania, US", new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1512), "18042", "N. Roehampton Ave." });

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "address_number", "city", "country", "created_or_updated", "postcode", "street" },
                values: new object[] { "57", "Bern", "Switzerland", new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1942), "3027", "Erlenweg" });

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "address_number", "city", "country", "created_or_updated", "postcode", "street" },
                values: new object[] { "3357", "Schenectady", "New York, US", new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1949), "12303", "Golden Ridge Road" });

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "address_number", "city", "country", "created_or_updated", "postcode", "street" },
                values: new object[] { "4597", "Fort Lauderdale", "Florida, US", new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1954), "33308", "Pointe Lane" });

            migrationBuilder.CreateIndex(
                name: "contact_uidx",
                table: "contacts",
                columns: new[] { "first_name", "surname", "street", "address_number", "postcode", "city", "country" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "contact_uidx",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "address_number",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "city",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "country",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "postcode",
                table: "contacts");

            migrationBuilder.DropColumn(
                name: "street",
                table: "contacts");

            migrationBuilder.CreateIndex(
                name: "contact_uidx",
                table: "contacts",
                columns: new[] { "first_name", "surname" });
        }
    }
}
