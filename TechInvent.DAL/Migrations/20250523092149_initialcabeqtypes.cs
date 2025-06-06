using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialcabeqtypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "cabinet_equipment_type",
                columns: new[] { "id_cabinet_equipment_type", "name" },
                values: new object[,]
                {
                    { 1, "Маршрутизатор" },
                    { 2, "Сервер" },
                    { 3, "Ноутбук" },
                    { 4, "Принтер" },
                    { 5, "Монитор" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "cabinet_equipment_type",
                keyColumn: "id_cabinet_equipment_type",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "cabinet_equipment_type",
                keyColumn: "id_cabinet_equipment_type",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "cabinet_equipment_type",
                keyColumn: "id_cabinet_equipment_type",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "cabinet_equipment_type",
                keyColumn: "id_cabinet_equipment_type",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "cabinet_equipment_type",
                keyColumn: "id_cabinet_equipment_type",
                keyValue: 5);
        }
    }
}
