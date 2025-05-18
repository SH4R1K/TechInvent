using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class cabinetequipmentworkplace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_workplace",
                table: "cabinet_equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_workplace",
                table: "cabinet_equipment",
                column: "id_workplace");

            migrationBuilder.AddForeignKey(
                name: "fk_cabinet_equipment_workplace",
                table: "cabinet_equipment",
                column: "id_workplace",
                principalTable: "workplace",
                principalColumn: "id_invent_stuff",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cabinet_equipment_workplace",
                table: "cabinet_equipment");

            migrationBuilder.DropIndex(
                name: "IX_cabinet_equipment_id_workplace",
                table: "cabinet_equipment");

            migrationBuilder.DropColumn(
                name: "id_workplace",
                table: "cabinet_equipment");
        }
    }
}
