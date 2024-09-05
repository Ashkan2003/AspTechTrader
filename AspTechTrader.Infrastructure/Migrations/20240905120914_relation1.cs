using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class relation1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    SymbolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: true),
                    LastDeal = table.Column<int>(type: "int", nullable: true),
                    LastDealPercentage = table.Column<float>(type: "real", nullable: true),
                    LastPrice = table.Column<int>(type: "int", nullable: true),
                    LastPricePercentage = table.Column<float>(type: "real", nullable: true),
                    TheFirst = table.Column<int>(type: "int", nullable: true),
                    TheLeast = table.Column<int>(type: "int", nullable: true),
                    TheMost = table.Column<int>(type: "int", nullable: true),
                    DemandVolume = table.Column<int>(type: "int", nullable: true),
                    DemandPrice = table.Column<int>(type: "int", nullable: true),
                    OfferPrice = table.Column<int>(type: "int", nullable: true),
                    OfferVolume = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: true),
                    ChartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.SymbolId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSymbol",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSymbol", x => new { x.SymbolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSymbol_Symbols_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbols",
                        principalColumn: "SymbolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSymbol_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSymbol_UserId",
                table: "UserSymbol",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSymbol");

            migrationBuilder.DropTable(
                name: "Symbols");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
