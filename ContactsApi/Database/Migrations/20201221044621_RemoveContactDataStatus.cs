using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApi.Database.Migrations
{
    public partial class RemoveContactDataStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact_data_status",
                table: "contact_data");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact_data_status",
                table: "contact_data",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "",
                comment: "Flag which tells if a contact data is active or marked as deleted");
        }
    }
}
