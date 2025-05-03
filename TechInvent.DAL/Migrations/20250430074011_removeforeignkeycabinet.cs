using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeforeignkeycabinet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "techrequest_cabinet",
                table: "tech_request");

            migrationBuilder.DropIndex(
                name: "IX_tech_request_id_cabinet",
                table: "tech_request");

            migrationBuilder.DropColumn(
                name: "id_cabinet",
                table: "tech_request");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_cabinet",
                table: "tech_request",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_id_cabinet",
                table: "tech_request",
                column: "id_cabinet");

            migrationBuilder.AddForeignKey(
                name: "techrequest_cabinet",
                table: "tech_request",
                column: "id_cabinet",
                principalTable: "cabinet",
                principalColumn: "id_cabinet");
        }
    }
}
