using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class removemonitors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monitor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monitor",
                columns: table => new
                {
                    id_invent_stuff = table.Column<int>(type: "int", nullable: false),
                    id_vendor = table.Column<int>(type: "int", nullable: true),
                    id_workplace = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_invent_stuff);
                    table.ForeignKey(
                        name: "FK_monitor_invent_stuff_id_invent_stuff",
                        column: x => x.id_invent_stuff,
                        principalTable: "invent_stuff",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_monitor_vendor",
                        column: x => x.id_vendor,
                        principalTable: "vendor",
                        principalColumn: "id_vendor");
                    table.ForeignKey(
                        name: "fk_monitor_workplace",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_monitor_id_vendor",
                table: "monitor",
                column: "id_vendor");

            migrationBuilder.CreateIndex(
                name: "IX_monitor_id_workplace",
                table: "monitor",
                column: "id_workplace");
        }
    }
}
