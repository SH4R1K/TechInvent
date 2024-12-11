using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id_role", "name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "user" }
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id_user", "id_role", "login", "password" },
                values: new object[] { 1, 1, "admin", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id_role",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "id_user",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id_role",
                keyValue: 1);
        }
    }
}
