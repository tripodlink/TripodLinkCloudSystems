using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class SeedGenlabAndBloodBankProgramMenus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "program_folder",
                columns: new[] { "pf_folder_id", "pf_icon", "pf_icon_provider", "pf_icon_type", "pf_module", "pf_module_route", "pf_folder_name", "pf_root", "pf_folder_route", "pf_seqno" },
                values: new object[,]
                {
                    { "GENSM", null, null, null, "GEN", "gen", "Sample Management", "PRG", "sample-management", 10 },
                    { "GENRM", null, null, null, "GEN", "gen", "Result Management", "PRG", "result-management", 10 },
                    { "BBSM", null, null, null, "BB", "gen", "Sample Management", "PRG", "sample-management", 10 },
                    { "BBRM", null, null, null, "BB", "bb", "Result Management", "PRG", "result-management", 10 }
                });

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

            migrationBuilder.InsertData(
                table: "program_menu",
                columns: new[] { "pm_id", "pm_action_route", "pm_controller_route", "pm_folder_route", "pm_icon_name", "pm_icon_provider", "pm_icon_type", "pm_module_route", "pm_name", "pm_folder", "pm_seqno" },
                values: new object[,]
                {
                    { "GENSM_SAMREC", "index", "sample-reception", "sample-management", null, null, null, "gen", "Sample Reception", "GENSM", 10 },
                    { "GENSM_SAMCHK", "index", "sample-checkin", "sample-management", null, null, null, "gen", "Sample Check-In", "GENSM", 20 },
                    { "GENSM_REJSAM", "index", "reject -sample", "sample-management", null, null, null, "gen", "Reject Sample", "GENSM", 30 },
                    { "GENSM_ADDELO", "index", "add-delete-lab-order", "sample-management", null, null, null, "gen", "Add/Delete Lab Order", "GENSM", 40 },
                    { "GENSM_DELLN", "index", "delete-lab-number", "sample-management", null, null, null, "gen", "Delete Lab Number", "GENSM", 50 },
                    { "GENRM_RESENT", "index", "result-entry", "result-management", null, null, null, "gen", "Result Entry", "GENRM", 10 },
                    { "GENRM_FPATREC", "index", "find-patient-record", "result-management", null, null, null, "gen", "Find Patient Record", "GENRM", 20 },
                    { "BBSM_SAMREC", "index", "sample-reception", "sample-management", null, null, null, "gen", "Sample Reception", "BBSM", 10 },
                    { "BBSM_SAMCHK", "index", "sample-checkin", "sample-management", null, null, null, "gen", "Sample Check-In", "BBSM", 20 },
                    { "BBSM_REJSAM", "index", "reject -sample", "sample-management", null, null, null, "gen", "Reject Sample", "BBSM", 30 },
                    { "BBSM_ADDELO", "index", "add-delete-lab-order", "sample-management", null, null, null, "gen", "Add/Delete Lab Order", "BBSM", 40 },
                    { "BBSM_DELLN", "index", "delete-lab-number", "sample-management", null, null, null, "gen", "Delete Lab Number", "BBSM", 50 },
                    { "BBRM_RESENT", "index", "result-entry", "result-management", null, null, null, "bb", "Result Entry", "BBRM", 10 },
                    { "BBRM_FPATREC", "index", "find-patient-record", "result-management", null, null, null, "gen", "Find Patient Record", "BBRM", 20 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBRM_FPATREC");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBRM_RESENT");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_ADDELO");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_DELLN");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_REJSAM");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMCHK");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "BBSM_SAMREC");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENRM_FPATREC");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENRM_RESENT");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENSM_ADDELO");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENSM_DELLN");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENSM_REJSAM");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENSM_SAMCHK");

            migrationBuilder.DeleteData(
                table: "program_menu",
                keyColumn: "pm_id",
                keyValue: "GENSM_SAMREC");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "BBRM");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "BBSM");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "GENRM");

            migrationBuilder.DeleteData(
                table: "program_folder",
                keyColumn: "pf_folder_id",
                keyValue: "GENSM");

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

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 7, 5, 45, 174, DateTimeKind.Local), new DateTime(2020, 1, 15, 7, 5, 45, 175, DateTimeKind.Local) });
        }
    }
}
