using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalon.Migrations
{
    /// <inheritdoc />
    public partial class UseFullAbilitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "OfflineGameProfileRoles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "OfflineGameProfileRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OfflineGameProfileRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "OfflineGameProfileRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "OfflineGameProfileRoles",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "OfflineGameMissions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OfflineGameMissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "OfflineGameMissions",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDateTime",
                table: "OfflineGameMissionProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDateTime",
                table: "OfflineGameMissionProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OfflineGameMissionProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDateTime",
                table: "OfflineGameMissionProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentity",
                table: "OfflineGameMissionProfiles",
                type: "nvarchar(450)",
                nullable: true,
                collation: "SQL_Latin1_General_CP1_CS_AS");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_CreationDateTime",
                table: "OfflineGameProfileRoles",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_DeletedDateTime",
                table: "OfflineGameProfileRoles",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_IsDeleted",
                table: "OfflineGameProfileRoles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_ModificationDateTime",
                table: "OfflineGameProfileRoles",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_UniqueIdentity",
                table: "OfflineGameProfileRoles",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_DeletedDateTime",
                table: "OfflineGameMissions",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_IsDeleted",
                table: "OfflineGameMissions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_UniqueIdentity",
                table: "OfflineGameMissions",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_CreationDateTime",
                table: "OfflineGameMissionProfiles",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_DeletedDateTime",
                table: "OfflineGameMissionProfiles",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_IsDeleted",
                table: "OfflineGameMissionProfiles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_ModificationDateTime",
                table: "OfflineGameMissionProfiles",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_UniqueIdentity",
                table: "OfflineGameMissionProfiles",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OfflineGameProfileRoles_CreationDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameProfileRoles_DeletedDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameProfileRoles_IsDeleted",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameProfileRoles_ModificationDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameProfileRoles_UniqueIdentity",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissions_DeletedDateTime",
                table: "OfflineGameMissions");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissions_IsDeleted",
                table: "OfflineGameMissions");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissions_UniqueIdentity",
                table: "OfflineGameMissions");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissionProfiles_CreationDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissionProfiles_DeletedDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissionProfiles_IsDeleted",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissionProfiles_ModificationDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropIndex(
                name: "IX_OfflineGameMissionProfiles_UniqueIdentity",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "OfflineGameProfileRoles");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "OfflineGameMissions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OfflineGameMissions");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "OfflineGameMissions");

            migrationBuilder.DropColumn(
                name: "CreationDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropColumn(
                name: "DeletedDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropColumn(
                name: "ModificationDateTime",
                table: "OfflineGameMissionProfiles");

            migrationBuilder.DropColumn(
                name: "UniqueIdentity",
                table: "OfflineGameMissionProfiles");
        }
    }
}
