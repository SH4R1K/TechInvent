using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class addTechRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tech_request",
                columns: table => new
                {
                    id_request = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "longtext", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_request);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "techrequest_has_workplace",
                columns: table => new
                {
                    id_workplace = table.Column<int>(type: "int", nullable: false),
                    id_request = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_request, x.id_workplace });
                    table.ForeignKey(
                        name: "fk_techrequest_workplace",
                        column: x => x.id_request,
                        principalTable: "tech_request",
                        principalColumn: "id_request");
                    table.ForeignKey(
                        name: "fk_workplace_techrequest",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_workplace");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_techrequest_has_workplace_id_workplace",
                table: "techrequest_has_workplace",
                column: "id_workplace");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "techrequest_has_workplace");

            migrationBuilder.DropTable(
                name: "tech_request");
        }
    }
}
