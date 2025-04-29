using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class uq_cabinet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "id_cabinet_UNIQUE",
                table: "cabinet");

            migrationBuilder.CreateIndex(
                name: "name_cabinet_UNIQUE",
                table: "cabinet",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "name_cabinet_UNIQUE",
                table: "cabinet");

            migrationBuilder.CreateIndex(
                name: "id_cabinet_UNIQUE",
                table: "cabinet",
                column: "id_cabinet",
                unique: true);
        }
    }
}
