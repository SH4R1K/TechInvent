using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class updateinventstuffuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_date",
                table: "user",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "decommission_comment",
                table: "invent_stuff",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "decommission_date",
                table: "invent_stuff",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id_role", "name" },
                values: new object[] { 3, "operator" });

            migrationBuilder.UpdateData(
                table: "user",
                keyColumn: "id_user",
                keyValue: 1,
                column: "last_login_date",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id_role",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "last_login_date",
                table: "user");

            migrationBuilder.DropColumn(
                name: "decommission_comment",
                table: "invent_stuff");

            migrationBuilder.DropColumn(
                name: "decommission_date",
                table: "invent_stuff");
        }
    }
}
