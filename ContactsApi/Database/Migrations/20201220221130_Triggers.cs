using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsApi.Database.Migrations
{
    public partial class Triggers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("CREATE FUNCTION created_or_updated()");
            stringBuilder.AppendLine("RETURNS trigger AS $created_or_updated$");
            stringBuilder.AppendLine("    BEGIN");
            stringBuilder.AppendLine("        NEW.created_or_updated := current_timestamp;");
            stringBuilder.AppendLine("        ");
            stringBuilder.AppendLine("        RETURN NEW;");
            stringBuilder.AppendLine("    END;");
            stringBuilder.AppendLine("$created_or_updated$ LANGUAGE plpgsql;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("CREATE TRIGGER contact_created_or_updated");
            stringBuilder.AppendLine("BEFORE INSERT OR UPDATE ON contacts");
            stringBuilder.AppendLine("FOR EACH ROW EXECUTE PROCEDURE created_or_updated();");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("CREATE TRIGGER contact_data_created_or_updated");
            stringBuilder.AppendLine("BEFORE INSERT OR UPDATE ON contact_data");
            stringBuilder.AppendLine("FOR EACH ROW EXECUTE PROCEDURE created_or_updated();");

            migrationBuilder.Sql(stringBuilder.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER contact_created_or_updated ON contacts;");

            migrationBuilder.Sql("DROP TRIGGER contact_data_created_or_updated ON contact_data;");

            migrationBuilder.Sql("DROP FUNCTION created_or_updated;");
        }
    }
}
