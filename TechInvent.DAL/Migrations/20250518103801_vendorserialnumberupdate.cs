using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class vendorserialnumberupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "serial_number",
                table: "monitor");

            migrationBuilder.AddColumn<int>(
                name: "id_vendor",
                table: "monitor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "serial_number",
                table: "invent_stuff",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_vendor",
                table: "cabinet_equipment",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cabinet",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "vendor",
                columns: table => new
                {
                    id_vendor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendor", x => x.id_vendor);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_monitor_id_vendor",
                table: "monitor",
                column: "id_vendor");

            migrationBuilder.CreateIndex(
                name: "uq_inventstuff_serialnumber",
                table: "invent_stuff",
                column: "serial_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_vendor",
                table: "cabinet_equipment",
                column: "id_vendor");

            migrationBuilder.CreateIndex(
                name: "name_unique",
                table: "vendor",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_cabinet_equipment_vendor",
                table: "cabinet_equipment",
                column: "id_vendor",
                principalTable: "vendor",
                principalColumn: "id_vendor");

            migrationBuilder.AddForeignKey(
                name: "fk_monitor_vendor",
                table: "monitor",
                column: "id_vendor",
                principalTable: "vendor",
                principalColumn: "id_vendor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cabinet_equipment_vendor",
                table: "cabinet_equipment");

            migrationBuilder.DropForeignKey(
                name: "fk_monitor_vendor",
                table: "monitor");

            migrationBuilder.DropTable(
                name: "vendor");

            migrationBuilder.DropIndex(
                name: "IX_monitor_id_vendor",
                table: "monitor");

            migrationBuilder.DropIndex(
                name: "uq_inventstuff_serialnumber",
                table: "invent_stuff");

            migrationBuilder.DropIndex(
                name: "IX_cabinet_equipment_id_vendor",
                table: "cabinet_equipment");

            migrationBuilder.DropColumn(
                name: "id_vendor",
                table: "monitor");

            migrationBuilder.DropColumn(
                name: "serial_number",
                table: "invent_stuff");

            migrationBuilder.DropColumn(
                name: "id_vendor",
                table: "cabinet_equipment");

            migrationBuilder.AddColumn<string>(
                name: "serial_number",
                table: "monitor",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cabinet",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);
        }
    }
}
