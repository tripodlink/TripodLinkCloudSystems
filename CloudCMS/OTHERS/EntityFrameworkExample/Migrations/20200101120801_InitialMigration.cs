using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkExample.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_account",
                columns: table => new
                {
                    ua_user_id = table.Column<string>(maxLength: 20, nullable: false),
                    ua_user_name = table.Column<string>(maxLength: 100, nullable: true),
                    ua_password = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account", x => x.ua_user_id);
                });

            migrationBuilder.InsertData(
                table: "user_account",
                columns: new[] { "ua_user_id", "ua_password", "ua_user_name" },
                values: new object[] { "SYSAD", ".00000", "SYSTEM ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_account");
        }
    }
}
