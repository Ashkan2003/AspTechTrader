using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Symbols",
                columns: new[] { "SymbolId", "ChartNumber", "DemandPrice", "DemandVolume", "LastDeal", "LastDealPercentage", "LastPrice", "LastPricePercentage", "OfferPrice", "OfferVolume", "State", "SymbolName", "TheFirst", "TheLeast", "TheMost", "Volume" },
                values: new object[,]
                {
                    { new Guid("1146184b-e8f3-4385-a43b-5fef1cbd17df"), "100", 100, 100, 100, 100f, 100, 100f, 100, 100, 1, "دارایکم", 100, 100, 100, 100 },
                    { new Guid("8605cd04-cade-48ef-ae79-d108652f93fb"), "100", 100, 100, 600, 100f, 150, 100f, 100, 100, 1, "اختم", 100, 110, 100, 120 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Symbols",
                keyColumn: "SymbolId",
                keyValue: new Guid("1146184b-e8f3-4385-a43b-5fef1cbd17df"));

            migrationBuilder.DeleteData(
                table: "Symbols",
                keyColumn: "SymbolId",
                keyValue: new Guid("8605cd04-cade-48ef-ae79-d108652f93fb"));
        }
    }
}
