using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rel4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWatchLists",
                columns: table => new
                {
                    UserWatchListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userWatchListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchLists", x => x.UserWatchListId);
                    table.ForeignKey(
                        name: "FK_UserWatchLists_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserWatchLists",
                columns: new[] { "UserWatchListId", "UserId", "userWatchListName" },
                values: new object[,]
                {
                    { new Guid("bc9e6ea8-f280-476e-8502-8d96926cde3e"), new Guid("43966394-6325-4e7d-a218-cf2d43faae24"), "سهام من" },
                    { new Guid("dbb733b5-86e1-4b9b-b1d7-e9d26d77165d"), new Guid("43966394-6325-4e7d-a218-cf2d43faae24"), "سهامppppp" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchLists_UserId",
                table: "UserWatchLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWatchLists");
        }
    }
}
