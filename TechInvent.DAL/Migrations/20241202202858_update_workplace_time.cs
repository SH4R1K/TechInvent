using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class update_workplace_time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                table: "workplace",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                table: "workplace");
        }
    }
}
