using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avalon.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFeedbackEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFeedbacks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UniqueIdentity = table.Column<string>(type: "nvarchar(450)", nullable: true, collation: "SQL_Latin1_General_CP1_CS_AS"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactInformation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFeedbacks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_CreationDateTime",
                table: "UserFeedbacks",
                column: "CreationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_DeletedDateTime",
                table: "UserFeedbacks",
                column: "DeletedDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_IsDeleted",
                table: "UserFeedbacks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_ModificationDateTime",
                table: "UserFeedbacks",
                column: "ModificationDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserFeedbacks_UniqueIdentity",
                table: "UserFeedbacks",
                column: "UniqueIdentity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFeedbacks");
        }
    }
}
