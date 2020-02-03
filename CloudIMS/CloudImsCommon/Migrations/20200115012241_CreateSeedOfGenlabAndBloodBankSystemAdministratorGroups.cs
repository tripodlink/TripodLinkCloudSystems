using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudCmsCommon.Migrations
{
    public partial class CreateSeedOfGenlabAndBloodBankSystemAdministratorGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 79, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 22, 40, 56, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 22, 40, 57, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "user_group",
                columns: new[] { "ug_id", "ug_module", "ug_name" },
                values: new object[,]
                {
                    { "GENADMIN", "GEN", "SYSTEM ADMINISTRATORS (GENLAB)" },
                    { "BBADMIN", "BB", "SYSTEM ADMINISTRATORS (BLOOD BANK)" }
                });

            migrationBuilder.InsertData(
                table: "user_group_programs",
                columns: new[] { "ugp_user_group", "ugp_program_id" },
                values: new object[,]
                {
                    { "GENADMIN", "BBSM_SAMREC" },
                    { "GENADMIN", "BBSM_SAMCHK" },
                    { "GENADMIN", "BBSM_REJSAM" },
                    { "GENADMIN", "BBSM_ADDELO" },
                    { "GENADMIN", "BBSM_DELLN" },
                    { "GENADMIN", "BBRM_RESENT" },
                    { "GENADMIN", "BBRM_FPATREC" },
                    { "BBADMIN", "BBSM_SAMREC" },
                    { "BBADMIN", "BBSM_SAMCHK" },
                    { "BBADMIN", "BBSM_REJSAM" },
                    { "BBADMIN", "BBSM_ADDELO" },
                    { "BBADMIN", "BBSM_DELLN" },
                    { "BBADMIN", "BBRM_RESENT" },
                    { "BBADMIN", "BBRM_FPATREC" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBRM_FPATREC" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBRM_RESENT" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBSM_ADDELO" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBSM_DELLN" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBSM_REJSAM" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBSM_SAMCHK" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "BBADMIN", "BBSM_SAMREC" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBRM_FPATREC" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBRM_RESENT" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBSM_ADDELO" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBSM_DELLN" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBSM_REJSAM" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBSM_SAMCHK" });

            migrationBuilder.DeleteData(
                table: "user_group_programs",
                keyColumns: new[] { "ugp_user_group", "ugp_program_id" },
                keyValues: new object[] { "GENADMIN", "BBSM_SAMREC" });

            migrationBuilder.DeleteData(
                table: "user_group",
                keyColumn: "ug_id",
                keyValue: "BBADMIN");

            migrationBuilder.DeleteData(
                table: "user_group",
                keyColumn: "ug_id",
                keyValue: "GENADMIN");

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
    }
}
