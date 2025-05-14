using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class addcabinetequipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cabinet_equipment_type",
                columns: table => new
                {
                    id_cabinet_equipment_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_cabinet_equipment_type);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cabinet_equipment",
                columns: table => new
                {
                    id_cabinet_equipment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    invent_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_cabinet = table.Column<int>(type: "int", nullable: true),
                    id_cabinet_equipment_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_cabinet_equipment);
                    table.ForeignKey(
                        name: "fk_cabinet_equipment_cabinet",
                        column: x => x.id_cabinet,
                        principalTable: "cabinet",
                        principalColumn: "id_cabinet",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_cabinet_equipment_type",
                        column: x => x.id_cabinet_equipment_type,
                        principalTable: "cabinet_equipment_type",
                        principalColumn: "id_cabinet_equipment_type");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_cabinet",
                table: "cabinet_equipment",
                column: "id_cabinet");

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_cabinet_equipment_type",
                table: "cabinet_equipment",
                column: "id_cabinet_equipment_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cabinet_equipment");

            migrationBuilder.DropTable(
                name: "cabinet_equipment_type");
        }
    }
}
