using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCmsCommon.Migrations
{
    public partial class CreateProgramMenuTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "program_root",
                columns: table => new
                {
                    pr_id = table.Column<string>(maxLength: 10, nullable: false),
                    pr_name = table.Column<string>(maxLength: 100, nullable: false),
                    pr_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_root", x => x.pr_id);
                });

            migrationBuilder.CreateTable(
                name: "program_folder",
                columns: table => new
                {
                    pf_folder_id = table.Column<string>(maxLength: 10, nullable: false),
                    pf_folder_name = table.Column<string>(maxLength: 100, nullable: false),
                    pf_root = table.Column<string>(maxLength: 10, nullable: false),
                    pf_module = table.Column<string>(maxLength: 10, nullable: false),
                    pf_module_route = table.Column<string>(maxLength: 10, nullable: false),
                    pf_folder_route = table.Column<string>(maxLength: 25, nullable: false),
                    pf_icon_type = table.Column<string>(maxLength: 10, nullable: true),
                    pf_icon_provider = table.Column<string>(maxLength: 20, nullable: true),
                    pf_icon = table.Column<string>(maxLength: 10, nullable: true),
                    pf_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_folder", x => x.pf_folder_id);
                    table.ForeignKey(
                        name: "FK_program_folder_program_root_pf_root",
                        column: x => x.pf_root,
                        principalTable: "program_root",
                        principalColumn: "pr_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "program_menu",
                columns: table => new
                {
                    pm_id = table.Column<string>(maxLength: 20, nullable: false),
                    pm_name = table.Column<string>(maxLength: 100, nullable: false),
                    pm_folder = table.Column<string>(maxLength: 10, nullable: false),
                    pm_module_route = table.Column<string>(maxLength: 10, nullable: false),
                    pm_folder_route = table.Column<string>(maxLength: 25, nullable: false),
                    pm_controller_route = table.Column<string>(maxLength: 25, nullable: false),
                    pm_action_route = table.Column<string>(maxLength: 25, nullable: false),
                    pm_icon_type = table.Column<string>(maxLength: 10, nullable: true),
                    pm_icon_provider = table.Column<string>(maxLength: 20, nullable: true),
                    pm_icon_name = table.Column<string>(maxLength: 100, nullable: true),
                    pm_seqno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program_menu", x => x.pm_id);
                    table.ForeignKey(
                        name: "FK_program_menu_program_folder_pm_folder",
                        column: x => x.pm_folder,
                        principalTable: "program_folder",
                        principalColumn: "pf_folder_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 188, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "program_root",
                columns: new[] { "pr_id", "pr_name", "pr_seqno" },
                values: new object[,]
                {
                    { "PRG", "Program", 100 },
                    { "SM", "System Management", 200 }
                });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 174, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "user_account",
                columns: new[] { "ua_user_id", "im_created_by", "im_created_on", "ua_is_active", "ua_is_mb_user", "ua_password", "im_updated_by", "im_updated_on", "ua_user_name" },
                values: new object[,]
                {
                    { "FGC", "SYSTEM", new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), (byte)1, (byte)1, ".00000", "SYSTEM", new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), "FROILAN G. CUIZON" },
                    { "MLS", "SYSTEM", new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), (byte)1, (byte)1, ".00000", "SYSTEM", new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), "MARKWIN L. SORIANO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_program_folder_pf_root",
                table: "program_folder",
                column: "pf_root");

            migrationBuilder.CreateIndex(
                name: "IX_program_menu_pm_folder",
                table: "program_menu",
                column: "pm_folder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "program_menu");

            migrationBuilder.DropTable(
                name: "program_folder");

            migrationBuilder.DropTable(
                name: "program_root");

            migrationBuilder.DeleteData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC");

            migrationBuilder.DeleteData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 720, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 14, 1, 56, 28, 694, DateTimeKind.Local), new DateTime(2020, 1, 14, 1, 56, 28, 696, DateTimeKind.Local) });
        }
    }
}
