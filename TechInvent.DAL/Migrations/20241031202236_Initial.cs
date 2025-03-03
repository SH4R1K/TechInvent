using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechInventAPI.Migrations
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
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
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
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_cabinet);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "manufacturer",
                columns: table => new
                {
                    id_manufacturer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
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
                name: "workplace",
                columns: table => new
                {
                    id_workplace = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    id_cabinet = table.Column<int>(type: "int", nullable: false),
                    id_os = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_workplace);
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
                name: "component",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    id_workplace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_hardware_component_workplace1",
                        column: x => x.id_workplace,
                        principalTable: "workplace",
                        principalColumn: "id_workplace",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disk",
                columns: table => new
                {
                    id_component = table.Column<int>(type: "int", nullable: false),
                    model = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
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
                    adapter_ram = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    uuid = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
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
                    serial_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
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
                    mac_address = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    id_manufacturer = table.Column<int>(type: "int", nullable: false),
                    adapter_type_id_adapter_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_component);
                    table.ForeignKey(
                        name: "fk_net_adapter_adapter_type1",
                        column: x => x.adapter_type_id_adapter_type,
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
                    physical_cores = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    logical_cores = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    max_clock_speed = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true)
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
                    speed = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    capacity = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    memory_type = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    part_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    serial_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE",
                table: "adapter_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_cabinet_UNIQUE",
                table: "cabinet",
                column: "id_cabinet",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_hardware_component_workplace1_idx",
                table: "component",
                column: "id_workplace");

            migrationBuilder.CreateIndex(
                name: "name_UNIQUE1",
                table: "manufacturer",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_net_adapter_adapter_type1_idx",
                table: "net_adapter",
                column: "adapter_type_id_adapter_type");

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
                name: "fk_workplace_cabinet_idx",
                table: "workplace",
                column: "id_cabinet");

            migrationBuilder.CreateIndex(
                name: "fk_workplace_os1_idx",
                table: "workplace",
                column: "id_os");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disk");

            migrationBuilder.DropTable(
                name: "gpu");

            migrationBuilder.DropTable(
                name: "mainboard");

            migrationBuilder.DropTable(
                name: "net_adapter");

            migrationBuilder.DropTable(
                name: "processor");

            migrationBuilder.DropTable(
                name: "ram");

            migrationBuilder.DropTable(
                name: "adapter_type");

            migrationBuilder.DropTable(
                name: "component");

            migrationBuilder.DropTable(
                name: "manufacturer");

            migrationBuilder.DropTable(
                name: "workplace");

            migrationBuilder.DropTable(
                name: "cabinet");

            migrationBuilder.DropTable(
                name: "os");
        }
    }
}
