using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class historyentityadded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Histories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ProjectId",
                table: "Histories",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Projects_ProjectId",
                table: "Histories",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Projects_ProjectId",
                table: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Histories_ProjectId",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Histories");
        }
    }
}
