using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    volume = table.Column<int>(type: "int", nullable: true),
                    lastDeal = table.Column<int>(type: "int", nullable: true),
                    lastDealPercentage = table.Column<float>(type: "real", nullable: true),
                    lastPrice = table.Column<int>(type: "int", nullable: true),
                    lastPricePercentage = table.Column<float>(type: "real", nullable: true),
                    theFirst = table.Column<int>(type: "int", nullable: true),
                    theLeast = table.Column<int>(type: "int", nullable: true),
                    theMost = table.Column<int>(type: "int", nullable: true),
                    demandVolume = table.Column<int>(type: "int", nullable: true),
                    demandPrice = table.Column<int>(type: "int", nullable: true),
                    offerPrice = table.Column<int>(type: "int", nullable: true),
                    offerVolume = table.Column<int>(type: "int", nullable: true),
                    state = table.Column<int>(type: "int", nullable: true),
                    chartNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.SymbolId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Symbols");
        }
    }
}
