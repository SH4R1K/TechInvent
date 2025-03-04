using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCabinetToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "tech_request",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "tech_request",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000);
        }
    }
}
