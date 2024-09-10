using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rel6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymbolUserWatchList",
                columns: table => new
                {
                    SymbolsSymbolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserWatchListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolUserWatchList", x => new { x.SymbolsSymbolId, x.UserWatchListId });
                    table.ForeignKey(
                        name: "FK_SymbolUserWatchList_Symbol_SymbolsSymbolId",
                        column: x => x.SymbolsSymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymbolUserWatchList_UserWatchLists_UserWatchListId",
                        column: x => x.UserWatchListId,
                        principalTable: "UserWatchLists",
                        principalColumn: "UserWatchListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SymbolUserWatchList_UserWatchListId",
                table: "SymbolUserWatchList",
                column: "UserWatchListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymbolUserWatchList");
        }
    }
}
