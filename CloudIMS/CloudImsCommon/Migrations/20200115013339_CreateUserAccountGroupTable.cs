using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudImsCommon.Migrations
{
    public partial class CreateUserAccountGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ugp_user_group",
                table: "user_group_programs",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ug_id",
                table: "user_group",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "user_account_group",
                columns: table => new
                {
                    UserAccountID = table.Column<string>(maxLength: 20, nullable: false),
                    UserGroupID = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_account_group", x => new { x.UserAccountID, x.UserGroupID });
                    table.ForeignKey(
                        name: "FK_user_account_group_user_account_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "user_account",
                        principalColumn: "ua_user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_account_group_user_group_UserGroupID",
                        column: x => x.UserGroupID,
                        principalTable: "user_group",
                        principalColumn: "ug_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "CBC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "FBS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "PRE-EMP-1",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "item_master",
                keyColumn: "im_code",
                keyValue: "URINA",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 335, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FGC",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "FUR",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "MLS",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "user_account",
                keyColumn: "ua_user_id",
                keyValue: "SYSAD",
                columns: new[] { "im_created_on", "im_updated_on" },
                values: new object[] { new DateTime(2020, 1, 15, 9, 33, 38, 319, DateTimeKind.Local), new DateTime(2020, 1, 15, 9, 33, 38, 320, DateTimeKind.Local) });

            migrationBuilder.CreateIndex(
                name: "IX_user_account_group_UserGroupID",
                table: "user_account_group",
                column: "UserGroupID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_account_group");

            migrationBuilder.AlterColumn<string>(
                name: "ugp_user_group",
                table: "user_group_programs",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "ug_id",
                table: "user_group",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

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
        }
    }
}
