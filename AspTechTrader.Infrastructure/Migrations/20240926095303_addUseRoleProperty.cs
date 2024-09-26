using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspTechTrader.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUseRoleProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("43966394-6325-4e7d-a218-cf2d43faae24"),
                column: "UserRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("70e41be8-ee03-4ed7-aa9e-7ad3d3b367b7"),
                column: "UserRole",
                value: 0);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("8d75d5bf-ea72-4f50-b72f-9e077a49518c"),
                column: "UserRole",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "User");
        }
    }
}
