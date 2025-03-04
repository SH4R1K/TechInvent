using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_techrequest_workplace",
                table: "techrequest_has_workplace");

            migrationBuilder.DropForeignKey(
                name: "fk_workplace_techrequest",
                table: "techrequest_has_workplace");

            migrationBuilder.AddColumn<int>(
                name: "IdRequestType",
                table: "tech_request",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "request_type",
                columns: table => new
                {
                    id_request_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_request_type);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "request_type",
                columns: new[] { "id_request_type", "name" },
                values: new object[,]
                {
                    { 1, "Проблема с ПК" },
                    { 2, "Проблема с периферией" },
                    { 3, "Проблема с ПО" },
                    { 4, "Проблема в кабинете" },
                    { 5, "Другое" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_IdRequestType",
                table: "tech_request",
                column: "IdRequestType");

            migrationBuilder.AddForeignKey(
                name: "techrequest_requesttype",
                table: "tech_request",
                column: "IdRequestType",
                principalTable: "request_type",
                principalColumn: "id_request_type");

            migrationBuilder.AddForeignKey(
                name: "fk_techrequest_workplace",
                table: "techrequest_has_workplace",
                column: "id_request",
                principalTable: "tech_request",
                principalColumn: "id_request",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_workplace_techrequest",
                table: "techrequest_has_workplace",
                column: "id_workplace",
                principalTable: "workplace",
                principalColumn: "id_workplace",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "techrequest_requesttype",
                table: "tech_request");

            migrationBuilder.DropForeignKey(
                name: "fk_techrequest_workplace",
                table: "techrequest_has_workplace");

            migrationBuilder.DropForeignKey(
                name: "fk_workplace_techrequest",
                table: "techrequest_has_workplace");

            migrationBuilder.DropTable(
                name: "request_type");

            migrationBuilder.DropIndex(
                name: "IX_tech_request_IdRequestType",
                table: "tech_request");

            migrationBuilder.DropColumn(
                name: "IdRequestType",
                table: "tech_request");

            migrationBuilder.AddForeignKey(
                name: "fk_techrequest_workplace",
                table: "techrequest_has_workplace",
                column: "id_request",
                principalTable: "tech_request",
                principalColumn: "id_request");

            migrationBuilder.AddForeignKey(
                name: "fk_workplace_techrequest",
                table: "techrequest_has_workplace",
                column: "id_workplace",
                principalTable: "workplace",
                principalColumn: "id_workplace");
        }
    }
}
