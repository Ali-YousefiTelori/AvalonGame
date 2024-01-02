﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Avalon.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMinionOfMordred = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    MinionOfMordredCount = table.Column<byte>(type: "tinyint", nullable: false),
                    MinionOfMerlinCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mission1PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mission2PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mission3PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mission4PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    Mission5PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    DoNeedsTwoOfFailsAtMission4 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfflineGames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StageId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfflineGames_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinishUpGames",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(type: "bigint", nullable: false),
                    OfflineGameId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinishUpGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinishUpGames_OfflineGames_OfflineGameId",
                        column: x => x.OfflineGameId,
                        principalTable: "OfflineGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinishUpGames_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfflineGameMissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfflineGameId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PlayerCount = table.Column<byte>(type: "tinyint", nullable: false),
                    DoNeedsTwoOfFails = table.Column<bool>(type: "bit", nullable: false),
                    IsFailed = table.Column<bool>(type: "bit", nullable: true),
                    FailCount = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineGameMissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfflineGameMissions_OfflineGames_OfflineGameId",
                        column: x => x.OfflineGameId,
                        principalTable: "OfflineGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfflineGameProfileRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfflineGameId = table.Column<long>(type: "bigint", nullable: false),
                    ProfileId = table.Column<long>(type: "bigint", nullable: false),
                    Roled = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineGameProfileRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfflineGameProfileRoles_OfflineGames_OfflineGameId",
                        column: x => x.OfflineGameId,
                        principalTable: "OfflineGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfflineGameProfileRoles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfflineGameProfileRoles_Roles_Roled",
                        column: x => x.Roled,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfflineGameMissionProfiles",
                columns: table => new
                {
                    OfflineGameMissionId = table.Column<long>(type: "bigint", nullable: false),
                    ProfileId = table.Column<long>(type: "bigint", nullable: false),
                    IsFail = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfflineGameMissionProfiles", x => new { x.OfflineGameMissionId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_OfflineGameMissionProfiles_OfflineGameMissions_OfflineGameMissionId",
                        column: x => x.OfflineGameMissionId,
                        principalTable: "OfflineGameMissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfflineGameMissionProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationDateTime", "DeletedDateTime", "IsDeleted", "IsMinionOfMordred", "ModificationDateTime", "Name", "UniqueIdentity" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "Merlin", null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "Percival", null },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Mordred", null },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Assassin", null },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Morgana", null },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Oberon", null },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 11L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 12L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, true, null, "Evil", null },
                    { 13L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 14L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 15L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 16L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 17L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 18L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 19L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null },
                    { 20L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, null, "People", null }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "Id", "CreationDateTime", "DeletedDateTime", "DoNeedsTwoOfFailsAtMission4", "IsDeleted", "MinionOfMerlinCount", "MinionOfMordredCount", "Mission1PlayerCount", "Mission2PlayerCount", "Mission3PlayerCount", "Mission4PlayerCount", "Mission5PlayerCount", "ModificationDateTime", "Name", "PlayerCount", "UniqueIdentity" },
                values: new object[,]
                {
                    { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, (byte)3, (byte)2, (byte)2, (byte)3, (byte)2, (byte)3, (byte)3, null, "5 Players", (byte)5, null },
                    { 2L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, false, (byte)4, (byte)2, (byte)2, (byte)3, (byte)4, (byte)3, (byte)4, null, "6 Players", (byte)6, null },
                    { 3L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)4, (byte)3, (byte)2, (byte)3, (byte)3, (byte)4, (byte)4, null, "7 Players", (byte)7, null },
                    { 4L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)5, (byte)3, (byte)3, (byte)4, (byte)4, (byte)5, (byte)5, null, "8 Players", (byte)8, null },
                    { 5L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)6, (byte)3, (byte)3, (byte)4, (byte)4, (byte)5, (byte)5, null, "9 Players", (byte)9, null },
                    { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)6, (byte)4, (byte)3, (byte)4, (byte)4, (byte)5, (byte)5, null, "10 Players", (byte)10, null },
                    { 7L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)6, (byte)5, (byte)3, (byte)4, (byte)4, (byte)5, (byte)6, null, "11 Players", (byte)11, null },
                    { 8L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)7, (byte)5, (byte)3, (byte)4, (byte)4, (byte)5, (byte)6, null, "12 Players", (byte)12, null },
                    { 9L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)7, (byte)6, (byte)3, (byte)4, (byte)4, (byte)5, (byte)7, null, "13 Players", (byte)13, null },
                    { 10L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)8, (byte)6, (byte)3, (byte)4, (byte)4, (byte)5, (byte)7, null, "14 Players", (byte)14, null },
                    { 11L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, (byte)8, (byte)7, (byte)3, (byte)4, (byte)4, (byte)5, (byte)8, null, "15 Players", (byte)15, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_CreationDateTime",
                table: "FinishUpGames",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_DeletedDateTime",
                table: "FinishUpGames",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_IsDeleted",
                table: "FinishUpGames",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_ModificationDateTime",
                table: "FinishUpGames",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_OfflineGameId",
                table: "FinishUpGames",
                column: "OfflineGameId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_ProfileId",
                table: "FinishUpGames",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_FinishUpGames_UniqueIdentity",
                table: "FinishUpGames",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissionProfiles_ProfileId",
                table: "OfflineGameMissionProfiles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_CreationDateTime",
                table: "OfflineGameMissions",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_ModificationDateTime",
                table: "OfflineGameMissions",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameMissions_OfflineGameId",
                table: "OfflineGameMissions",
                column: "OfflineGameId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_OfflineGameId",
                table: "OfflineGameProfileRoles",
                column: "OfflineGameId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_ProfileId",
                table: "OfflineGameProfileRoles",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGameProfileRoles_Roled",
                table: "OfflineGameProfileRoles",
                column: "Roled");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_CreationDateTime",
                table: "OfflineGames",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_DeletedDateTime",
                table: "OfflineGames",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_IsDeleted",
                table: "OfflineGames",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_ModificationDateTime",
                table: "OfflineGames",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_StageId",
                table: "OfflineGames",
                column: "StageId");

            migrationBuilder.CreateIndex(
                name: "IX_OfflineGames_UniqueIdentity",
                table: "OfflineGames",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CreationDateTime",
                table: "Profiles",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_DeletedDateTime",
                table: "Profiles",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_IsDeleted",
                table: "Profiles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_ModificationDateTime",
                table: "Profiles",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UniqueIdentity",
                table: "Profiles",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreationDateTime",
                table: "Roles",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedDateTime",
                table: "Roles",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDeleted",
                table: "Roles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ModificationDateTime",
                table: "Roles",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UniqueIdentity",
                table: "Roles",
                column: "UniqueIdentity");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_CreationDateTime",
                table: "Stages",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_DeletedDateTime",
                table: "Stages",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_IsDeleted",
                table: "Stages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_ModificationDateTime",
                table: "Stages",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Stages_UniqueIdentity",
                table: "Stages",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinishUpGames");

            migrationBuilder.DropTable(
                name: "OfflineGameMissionProfiles");

            migrationBuilder.DropTable(
                name: "OfflineGameProfileRoles");

            migrationBuilder.DropTable(
                name: "OfflineGameMissions");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "OfflineGames");

            migrationBuilder.DropTable(
                name: "Stages");
        }
    }
}
