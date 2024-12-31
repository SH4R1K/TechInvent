using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class cascade_for_installedsoft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workplace_installed_software",
                table: "installed_software");

            migrationBuilder.AddForeignKey(
                name: "fk_workplace_installed_software",
                table: "installed_software",
                column: "id_workplace",
                principalTable: "workplace",
                principalColumn: "id_workplace",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workplace_installed_software",
                table: "installed_software");

            migrationBuilder.AddForeignKey(
                name: "fk_workplace_installed_software",
                table: "installed_software",
                column: "id_workplace",
                principalTable: "workplace",
                principalColumn: "id_workplace");
        }
    }
}
