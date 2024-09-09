using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "EmailAddress", "UserName", "UserProperty" },
                values: new object[,]
                {
                    { new Guid("43966394-6325-4e7d-a218-cf2d43faae24"), "amin@email.com", "amin", 10 },
                    { new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7"), "jonas@email.com", "jonas", 1160 },
                    { new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c"), "ashkan@email.com", "ashkan", 160 }
                });

            migrationBuilder.InsertData(
                table: "UserSymbolProperty",
                columns: new[] { "UserSymbolPropertyId", "SymbolId", "SymbolPrice", "SymbolQuantity", "UserId" },
                values: new object[,]
                {
                    { new Guid("2af3944d-8dfa-4262-a9e0-bb96ad2c7166"), new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"), 40, 170, new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c") },
                    { new Guid("61b5469a-ee2e-4083-ab6a-975c8d2e4fe3"), new Guid("df32447c-7578-46af-a77a-73efd458c201"), 450, 1370, new Guid("43966394-6325-4e7d-a218-cf2d43faae24") },
                    { new Guid("98818f76-e989-4be4-afb0-d61db4bbd590"), new Guid("df32447c-7578-46af-a77a-73efd458c201"), 450, 1370, new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c") },
                    { new Guid("a7d97c83-0c10-45bf-94e7-05c43d59257a"), new Guid("c39734d9-125a-43fa-88fe-8e12a209f1b1"), 450, 1370, new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserSymbolProperty",
                keyColumn: "UserSymbolPropertyId",
                keyValue: new Guid("2af3944d-8dfa-4262-a9e0-bb96ad2c7166"));

            migrationBuilder.DeleteData(
                table: "UserSymbolProperty",
                keyColumn: "UserSymbolPropertyId",
                keyValue: new Guid("61b5469a-ee2e-4083-ab6a-975c8d2e4fe3"));

            migrationBuilder.DeleteData(
                table: "UserSymbolProperty",
                keyColumn: "UserSymbolPropertyId",
                keyValue: new Guid("98818f76-e989-4be4-afb0-d61db4bbd590"));

            migrationBuilder.DeleteData(
                table: "UserSymbolProperty",
                keyColumn: "UserSymbolPropertyId",
                keyValue: new Guid("a7d97c83-0c10-45bf-94e7-05c43d59257a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("43966394-6325-4e7d-a218-cf2d43faae24"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c"));
        }
    }
}
