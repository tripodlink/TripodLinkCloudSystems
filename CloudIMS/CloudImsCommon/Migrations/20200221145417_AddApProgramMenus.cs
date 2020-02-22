using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddApProgramMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "company",
                keyColumn: "id",
                keyValue: "000",
                column: "company_name",
                value: "NEW CLOUD IMS CUSTOMER");

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

            migrationBuilder.InsertData(
                table: "program_folder",
                columns: new[] { "pf_folder_id", "pf_icon", "pf_icon_provider", "pf_icon_type", "pf_module", "pf_module_route", "pf_folder_name", "pf_root", "pf_folder_route", "pf_seqno" },
                values: new object[,]
                {
                    { "APSM", null, null, null, "AP", "ap", "Sample Management", "PRG", "sample-management", 10 },
                    { "APRM", null, null, null, "AP", "ap", "Result Management", "PRG", "result-management", 20 }
                });

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

            migrationBuilder.InsertData(
                table: "program_menu",
                columns: new[] { "pm_id", "pm_action_route", "pm_controller_route", "pm_folder_route", "pm_icon_name", "pm_icon_provider", "pm_icon_type", "pm_module_route", "pm_name", "pm_folder", "pm_seqno" },
                values: new object[,]
                {
                    { "APSM_SAMREC", "index", "sample-reception", "sample-management", null, null, null, "ap", "Sample Reception", "APSM", 10 },
                    { "APSM_BLKSLD", "index", "block-and-slide", "sample-management", null, null, null, "ap", "Block & Slide", "APSM", 20 },
                    { "APRM_RESENT", "index", "ap-result-entry", "result-management", null, null, null, "ap", "AP Result Entry", "APRM", 10 },
                    { "APRM_PAPRES", "index", "pap-smear-result-entry", "result-management", null, null, null, "ap", "Pap Smear Result Enter", "APRM", 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "APRM_PAPRES");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "APRM_RESENT");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "APSM_BLKSLD");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "APSM_SAMREC");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "APRM");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "APSM");

            migrationBuilder.UpdateData(
                table: "company",
                keyColumn: "id",
                keyValue: "000",
                column: "company_name",
                value: "NEW CLOUD CMS CUSTOMER");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 503, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 47, 45, 479, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 47, 45, 481, DateTimeKind.Local) });
        }
    }
}
