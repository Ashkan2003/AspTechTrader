using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Symbol",
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
                    table.PrimaryKey("PK_Symbol", x => x.SymbolId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProperty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSymbolProperty",
                columns: table => new
                {
                    UserSymbolPropertyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolPrice = table.Column<int>(type: "int", nullable: false),
                    SymbolQuantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SymbolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSymbolProperty", x => x.UserSymbolPropertyId);
                    table.ForeignKey(
                        name: "FK_UserSymbolProperty_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbol",
                        principalColumn: "SymbolId");
                    table.ForeignKey(
                        name: "FK_UserSymbolProperty_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.InsertData(
                table: "Symbol",
                columns: new[] { "SymbolId", "ChartNumber", "DemandPrice", "DemandVolume", "LastDeal", "LastDealPercentage", "LastPrice", "LastPricePercentage", "OfferPrice", "OfferVolume", "State", "SymbolName", "TheFirst", "TheLeast", "TheMost", "Volume" },
                values: new object[,]
                {
                    { new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"), "100", 100, 100, 100, 100f, 100, 100f, 100, 100, 1, "دارایکم", 100, 100, 100, 100 },
                    { new Guid("df32447c-7578-46af-a77a-73efd458c201"), "100", 100, 100, 600, 100f, 150, 100f, 100, 100, 1, "اختم", 100, 110, 100, 120 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSymbolProperty_SymbolId",
                table: "UserSymbolProperty",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSymbolProperty_UserId",
                table: "UserSymbolProperty",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSymbolProperty");

            migrationBuilder.DropTable(
                name: "Symbol");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
