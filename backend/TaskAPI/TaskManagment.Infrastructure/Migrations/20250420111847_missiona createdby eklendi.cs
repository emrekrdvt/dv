using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class missionacreatedbyeklendi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_UserId",
                table: "Missions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Users_UserId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_UserId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Missions");
        }
    }
}
