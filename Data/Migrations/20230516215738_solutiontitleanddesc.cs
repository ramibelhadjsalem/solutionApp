using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace solutionApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class solutiontitleanddesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Solutions",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Solutions",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Solutions");
        }
    }
}
