using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class CreateUserGroupTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_group",
                columns: table => new
                {
                    ug_id = table.Column<string>(maxLength: 10, nullable: false),
                    ug_name = table.Column<string>(maxLength: 100, nullable: false),
                    ug_module = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group", x => x.ug_id);
                });

            migrationBuilder.CreateTable(
                name: "user_group_programs",
                columns: table => new
                {
                    ugp_user_group = table.Column<string>(maxLength: 10, nullable: false),
                    ugp_program_id = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group_programs", x => new { x.ugp_user_group, x.ugp_program_id });
                    table.UniqueConstraint("AK_user_group_programs_ugp_program_id_ugp_user_group", x => new { x.ugp_program_id, x.ugp_user_group });
                    table.ForeignKey(
                        name: "FK_user_group_programs_program_menu_ugp_program_id",
                        column: x => x.ugp_program_id,
                        principalTable: "program_menu",
                        principalColumn: "pm_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_group_programs_user_group_ugp_user_group",
                        column: x => x.ugp_user_group,
                        principalTable: "user_group",
                        principalColumn: "ug_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 481, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 482, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 6, 39, 468, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 6, 39, 469, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_group_programs");

            migrationBuilder.DropTable(
                name: "user_group");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 820, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 820, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 821, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 14, 31, 801, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 14, 31, 802, DateTimeKind.Local) });
        }
    }
}
