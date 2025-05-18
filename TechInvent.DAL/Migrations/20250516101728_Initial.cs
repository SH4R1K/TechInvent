using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechInvent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "adapter_type",
                columns: table => new
                {
                    id_adapter_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_adapter_type);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cabinet",
                columns: table => new
                {
                    id_cabinet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_cabinet);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                name: "invent_stuff",
                columns: table => new
                {
                    id_invent_stuff = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    invent_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    is_decommissioned = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_invent_stuff);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id_manufacturer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_manufacturer);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "os",
                columns: table => new
                {
                    id_os = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    os_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    os_version = table.Column<string>(type: "varchar(125)", maxLength: 125, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_os);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "request_type",
                columns: table => new
                {
                    id_request_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_request_type);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id_role = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_role);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cabinet_equipment",
                columns: table => new
                {
                    id_invent_stuff = table.Column<int>(type: "int", nullable: false),
                    id_cabinet = table.Column<int>(type: "int", nullable: true),
                    id_cabinet_equipment_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_invent_stuff);
                    table.ForeignKey(
                        name: "FK_cabinet_equipment_invent_stuff_id_invent_stuff",
                        column: x => x.id_invent_stuff,
                        principalTable: "invent_stuff",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "software",
                columns: table => new
                {
                    id_software = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    version = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    id_manufacturer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_software);
                    table.ForeignKey(
                        name: "fk_software_manufacturer",
                        column: x => x.id_manufacturer,
                        principalTable: "manufacturer",
                        principalColumn: "id_manufacturer");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "workplace",
                columns: table => new
                {
                    id_invent_stuff = table.Column<int>(type: "int", nullable: false),
                    id_cabinet = table.Column<int>(type: "int", nullable: false),
                    id_os = table.Column<int>(type: "int", nullable: false),
                    guid = table.Column<Guid>(type: "char(36)", nullable: false),
                    last_update = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_invent_stuff);
                    table.ForeignKey(
                        name: "FK_workplace_invent_stuff_id_invent_stuff",
                        column: x => x.id_invent_stuff,
                        principalTable: "invent_stuff",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workplace_cabinet",
                        column: x => x.id_cabinet,
                        principalTable: "cabinet",
                        principalColumn: "id_cabinet");
                    table.ForeignKey(
                        name: "fk_workplace_os1",
                        column: x => x.id_os,
                        principalTable: "os",
                        principalColumn: "id_os");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    id_role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_user);
                    table.ForeignKey(
                        name: "fk_user_role",
                        column: x => x.id_role,
                        principalTable: "role",
                        principalColumn: "id_role");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "component",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    id_workplace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_hardware_component_workplace1",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "installed_software",
                columns: table => new
                {
                    id_workplace = table.Column<int>(type: "int", nullable: false),
                    id_software = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_software, x.id_workplace });
                    table.ForeignKey(
                        name: "fk_software_installed_software",
                        column: x => x.id_software,
                        principalTable: "software",
                        principalColumn: "id_software");
                    table.ForeignKey(
                        name: "fk_workplace_installed_software",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "monitor",
                columns: table => new
                {
                    id_invent_stuff = table.Column<int>(type: "int", nullable: false),
                    serial_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_workplace = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_invent_stuff);
                    table.ForeignKey(
                        name: "FK_monitor_invent_stuff_id_invent_stuff",
                        column: x => x.id_invent_stuff,
                        principalTable: "invent_stuff",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_monitor_workplace",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tech_request",
                columns: table => new
                {
                    id_request = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_request_type = table.Column<int>(type: "int", nullable: false),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_request);
                    table.ForeignKey(
                        name: "techrequest_requesttype",
                        column: x => x.id_request_type,
                        principalTable: "request_type",
                        principalColumn: "id_request_type");
                    table.ForeignKey(
                        name: "techrequest_user",
                        column: x => x.id_user,
                        principalTable: "user",
                        principalColumn: "id_user");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disk",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_disk_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gpu",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    adapter_ram = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    uuid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_gpu_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mainboard",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    serial_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_mainboard_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "net_adapter",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    mac_address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_manufacturer = table.Column<int>(type: "int", nullable: false),
                    id_adapter_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_net_adapter_adapter_type1",
                        column: x => x.id_adapter_type,
                        principalTable: "adapter_type",
                        principalColumn: "id_adapter_type");
                    table.ForeignKey(
                        name: "fk_net_adapter_manufacturer2",
                        column: x => x.id_manufacturer,
                        principalTable: "manufacturer",
                        principalColumn: "id_manufacturer");
                    table.ForeignKey(
                        name: "fk_ram_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "processor",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    physical_cores = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    logical_cores = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    max_clock_speed = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_processor_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ram",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    speed = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    capacity = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    memory_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    part_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    serial_number = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    id_manufacturer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_net_adapter_component1",
                        column: x => x.id_component,
                        principalTable: "component",
                        principalColumn: "id_component",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_net_adapter_manufacturer1",
                        column: x => x.id_manufacturer,
                        principalTable: "manufacturer",
                        principalColumn: "id_manufacturer");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "techrequest_has_workplace",
                columns: table => new
                {
                    id_workplace = table.Column<int>(type: "int", nullable: false),
                    id_request = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_request, x.id_workplace });
                    table.ForeignKey(
                        name: "fk_techrequest_workplace",
                        column: x => x.id_request,
                        principalTable: "tech_request",
                        principalColumn: "id_request",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workplace_techrequest",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_invent_stuff",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "request_type",
                columns: new[] { "id_request_type", "name" },
                values: new object[,]
                {
                    { 1, "Проблема с ПК" },
                    { 2, "Проблема с периферией" },
                    { 3, "Проблема с ПО" },
                    { 4, "Проблема в кабинете" },
                    { 5, "Другое" }
                });

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

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE",
                table: "adapter_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "name_cabinet_UNIQUE",
                table: "cabinet",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_cabinet",
                table: "cabinet_equipment",
                column: "id_cabinet");

            migrationBuilder.CreateIndex(
                name: "IX_cabinet_equipment_id_cabinet_equipment_type",
                table: "cabinet_equipment",
                column: "id_cabinet_equipment_type");

            migrationBuilder.CreateIndex(
                name: "fk_hardware_component_workplace1_idx",
                table: "component",
                column: "id_workplace");

            migrationBuilder.CreateIndex(
                name: "IX_installed_software_id_workplace",
                table: "installed_software",
                column: "id_workplace");

            migrationBuilder.CreateIndex(
                name: "uq_inventstuff_inventnumber",
                table: "invent_stuff",
                column: "invent_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE1",
                table: "manufacturer",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_monitor_id_workplace",
                table: "monitor",
                column: "id_workplace");

            migrationBuilder.CreateIndex(
                name: "fk_net_adapter_adapter_type1_idx",
                table: "net_adapter",
                column: "id_adapter_type");

            migrationBuilder.CreateIndex(
                name: "fk_net_adapter_manufacturer2_idx",
                table: "net_adapter",
                column: "id_manufacturer");

            migrationBuilder.CreateIndex(
                name: "fk_ram_component1_idx",
                table: "net_adapter",
                column: "id_component");

            migrationBuilder.CreateIndex(
                name: "UQ_name_version",
                table: "os",
                columns: new[] { "os_name", "os_version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_net_adapter_manufacturer1_idx",
                table: "ram",
                column: "id_manufacturer");

            migrationBuilder.CreateIndex(
                name: "fk_software_manufacturer",
                table: "software",
                column: "id_manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_id_request_type",
                table: "tech_request",
                column: "id_request_type");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_id_user",
                table: "tech_request",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_comment_id_request",
                table: "tech_request_comment",
                column: "id_request");

            migrationBuilder.CreateIndex(
                name: "IX_tech_request_comment_id_user",
                table: "tech_request_comment",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_techrequest_has_workplace_id_workplace",
                table: "techrequest_has_workplace",
                column: "id_workplace");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_role",
                table: "user",
                column: "id_role");

            migrationBuilder.CreateIndex(
                name: "fk_workplace_cabinet_idx",
                table: "workplace",
                column: "id_cabinet");

            migrationBuilder.CreateIndex(
                name: "fk_workplace_os1_idx",
                table: "workplace",
                column: "id_os");

            migrationBuilder.CreateIndex(
                name: "uq_workplace_guid",
                table: "workplace",
                column: "guid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cabinet_equipment");

            migrationBuilder.DropTable(
                name: "disk");

            migrationBuilder.DropTable(
                name: "gpu");

            migrationBuilder.DropTable(
                name: "installed_software");

            migrationBuilder.DropTable(
                name: "mainboard");

            migrationBuilder.DropTable(
                name: "monitor");

            migrationBuilder.DropTable(
                name: "net_adapter");

            migrationBuilder.DropTable(
                name: "processor");

            migrationBuilder.DropTable(
                name: "ram");

            migrationBuilder.DropTable(
                name: "tech_request_comment");

            migrationBuilder.DropTable(
                name: "techrequest_has_workplace");

            migrationBuilder.DropTable(
                name: "cabinet_equipment_type");

            migrationBuilder.DropTable(
                name: "software");

            migrationBuilder.DropTable(
                name: "adapter_type");

            migrationBuilder.DropTable(
                name: "component");

            migrationBuilder.DropTable(
                name: "tech_request");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "workplace");

            migrationBuilder.DropTable(
                name: "request_type");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "invent_stuff");

            migrationBuilder.DropTable(
                name: "cabinet");

            migrationBuilder.DropTable(
                name: "os");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
