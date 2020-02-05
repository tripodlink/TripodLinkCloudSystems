using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class AddSeedOfUnitCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 37, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "unit_code",
                columns: new[] { "uc_code", "uc_description" },
                values: new object[,]
                {
                    { "100", "Meter" },
                    { "101", "Kilo" }
                });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 38, 12, 15, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 38, 12, 17, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IDX_DESCRIPTION",
                table: "unit_code",
                column: "uc_description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IDX_DESCRIPTION",
                table: "unit_code");

            migrationBuilder.DeleteData(
                table: "unit_code",
                keyColumn: "uc_code",
                keyValue: "100");

            migrationBuilder.DeleteData(
                table: "unit_code",
                keyColumn: "uc_code",
                keyValue: "101");

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 87, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 2, 5, 23, 26, 40, 66, DateTimeKind.Local), new DateTime(2020, 2, 5, 23, 26, 40, 67, DateTimeKind.Local) });
        }
    }
}
