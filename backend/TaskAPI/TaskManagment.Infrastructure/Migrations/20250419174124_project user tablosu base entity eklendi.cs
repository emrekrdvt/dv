using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class projectusertablosubaseentityeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProjectUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProjectUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProjectUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProjectUsers");
        }
    }
}
