using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInventAPI.Migrations
{
    /// <inheritdoc />
    public partial class addcommentstechrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tech_request_comment",
                columns: table => new
                {
                    id_comment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    id_request = table.Column<int>(type: "int", nullable: false),
                    comment_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    message = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_comment);
                    table.ForeignKey(
                        name: "fk_techrequest_comment",
                        column: x => x.id_request,
                        principalTable: "tech_request",
                        principalColumn: "id_request",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_comments",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_comment_id_request",
                table: "tech_request_comment",
                column: "id_request");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_comment_id_user",
                table: "tech_request_comment",
                column: "id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tech_request_comment");
        }
    }
}
