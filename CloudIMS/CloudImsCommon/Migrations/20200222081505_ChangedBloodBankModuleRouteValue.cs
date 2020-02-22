using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class ChangedBloodBankModuleRouteValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 385, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 385, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 386, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_ADDELO",
                column: "pm_module_route",
                value: "bb");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_DELLN",
                column: "pm_module_route",
                value: "bb");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_REJSAM",
                column: "pm_module_route",
                value: "bb");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMCHK",
                column: "pm_module_route",
                value: "bb");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMREC",
                column: "pm_module_route",
                value: "bb");

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 22, 16, 15, 4, 365, DateTimeKind.Local), new DateTime(2020, 2, 22, 16, 15, 4, 367, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 448, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_ADDELO",
                column: "pm_module_route",
                value: "gen");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_DELLN",
                column: "pm_module_route",
                value: "gen");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_REJSAM",
                column: "pm_module_route",
                value: "gen");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMCHK",
                column: "pm_module_route",
                value: "gen");

            migrationBuilder.UpdateData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMREC",
                column: "pm_module_route",
                value: "gen");

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 21, 22, 54, 16, 427, DateTimeKind.Local), new DateTime(2020, 2, 21, 22, 54, 16, 428, DateTimeKind.Local) });
        }
    }
}
