using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class addmonitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monitor",
                columns: table => new
                {
                    id_monitor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    invent_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_workplace = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_monitor);
                    table.ForeignKey(
                        name: "fk_monitor_workplace",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_workplace",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_monitor_id_workplace",
                table: "monitor",
                column: "id_workplace");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monitor");
        }
    }
}
