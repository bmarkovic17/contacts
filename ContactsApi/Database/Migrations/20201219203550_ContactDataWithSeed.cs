using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContactsApi.Database.Migrations
{
    public partial class ContactDataWithSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "contacts",
                comment: "Basic information about contacts",
                oldComment: "Data about contacts");

            migrationBuilder.CreateTable(
                name: "contact_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contact_data_type = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false, comment: "Code which designates type of contact data (e.g. phone, mail, ...)"),
                    contact_data_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false, comment: "Flag which tells if a contact data is active or marked as deleted"),
                    contact_data_value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "Concrete value of contact data (e.g. contact's phone number)"),
                    created_or_updated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact_data", x => x.id);
                    table.UniqueConstraint("ak_contact_data_contact_id_contact_data_type_contact_data_value", x => new { x.contact_id, x.contact_data_type, x.contact_data_value });
                    table.ForeignKey(
                        name: "fk_contact_data_contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Additional information about contacts");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contact_data");

            migrationBuilder.AlterTable(
                name: "contacts",
                comment: "Data about contacts",
                oldComment: "Basic information about contacts");

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 1,
                column: "created_or_updated",
                value: new DateTime(2020, 12, 19, 20, 48, 38, 671, DateTimeKind.Local).AddTicks(8786));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 2,
                column: "created_or_updated",
                value: new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1512));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 3,
                column: "created_or_updated",
                value: new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1942));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 4,
                column: "created_or_updated",
                value: new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1949));

            migrationBuilder.UpdateData(
                table: "contacts",
                keyColumn: "id",
                keyValue: 5,
                column: "created_or_updated",
                value: new DateTime(2020, 12, 19, 20, 48, 38, 675, DateTimeKind.Local).AddTicks(1954));
        }
    }
}
