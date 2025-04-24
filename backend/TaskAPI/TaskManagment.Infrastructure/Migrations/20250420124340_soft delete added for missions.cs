using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softdeleteaddedformissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Missions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Missions");
        }
    }
}
