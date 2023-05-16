using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solutionApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class techUsermig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_AspNetUsers_TechUserId",
                table: "Reclamations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_AspNetUsers_TechUserId",
                table: "Reclamations",
                column: "TechUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_AspNetUsers_TechUserId",
                table: "Reclamations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_AspNetUsers_TechUserId",
                table: "Reclamations",
                column: "TechUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
