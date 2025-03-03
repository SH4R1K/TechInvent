using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class guids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "guid",
                table: "workplace",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "uq_workplace_guid",
                table: "workplace",
                column: "guid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uq_workplace_guid",
                table: "workplace");

            migrationBuilder.DropColumn(
                name: "guid",
                table: "workplace");
        }
    }
}
