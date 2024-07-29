using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmerBank.Migrations
{
    /// <inheritdoc />
    public partial class delteAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ApplicationProfiles_AppUserProfileID",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationProfiles_ApplicationUsers_AppUserID",
                table: "ApplicationProfiles");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationProfiles_AppUserID",
                table: "ApplicationProfiles");

            migrationBuilder.DropColumn(
                name: "AppUserID",
                table: "ApplicationProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserProfileID",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_ApplicationProfiles_AppUserProfileID",
                table: "Accounts",
                column: "AppUserProfileID",
                principalTable: "ApplicationProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_ApplicationProfiles_AppUserProfileID",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AppUserID",
                table: "ApplicationProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserProfileID",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationProfiles_AppUserID",
                table: "ApplicationProfiles",
                column: "AppUserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_ApplicationProfiles_AppUserProfileID",
                table: "Accounts",
                column: "AppUserProfileID",
                principalTable: "ApplicationProfiles",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationProfiles_ApplicationUsers_AppUserID",
                table: "ApplicationProfiles",
                column: "AppUserID",
                principalTable: "ApplicationUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
