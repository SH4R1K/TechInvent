using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class techrequestuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_user",
                table: "tech_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_id_user",
                table: "tech_request",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "techrequest_user",
                table: "tech_request",
                column: "id_user",
                principalTable: "user",
                principalColumn: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "techrequest_user",
                table: "tech_request");

            migrationBuilder.DropIndex(
                name: "IX_tech_request_id_user",
                table: "tech_request");

            migrationBuilder.DropColumn(
                name: "id_user",
                table: "tech_request");
        }
    }
}
