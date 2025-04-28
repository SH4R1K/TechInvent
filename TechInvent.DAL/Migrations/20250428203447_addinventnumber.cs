using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class addinventnumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invent_number",
                table: "workplace",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invent_number",
                table: "workplace");
        }
    }
}
