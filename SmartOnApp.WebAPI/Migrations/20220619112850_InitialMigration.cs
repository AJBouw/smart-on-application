using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartOnApp.WebAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "mcu",
                columns: table => new
                {
                    mcu_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn),
                    mcu_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mcu_mac_address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mcu_ip = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mcu_hostname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mcu", x => x.mcu_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "iot_device",
                columns: table => new
                {
                    iot_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    iot_device_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    iot_device_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mcu_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iot_device", x => x.iot_device_id);
                    table.ForeignKey(
                        name: "FK_iot_device_mcu_mcu_id",
                        column: x => x.mcu_id,
                        principalTable: "mcu",
                        principalColumn: "mcu_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ldr",
                columns: table => new
                {
                    iot_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    brightness = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ldr", x => x.iot_device_id);
                    table.ForeignKey(
                        name: "FK_ldr_iot_device_iot_device_id",
                        column: x => x.iot_device_id,
                        principalTable: "iot_device",
                        principalColumn: "iot_device_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "light",
                columns: table => new
                {
                    iot_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    light_is_on = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_light", x => x.iot_device_id);
                    table.ForeignKey(
                        name: "FK_light_iot_device_iot_device_id",
                        column: x => x.iot_device_id,
                        principalTable: "iot_device",
                        principalColumn: "iot_device_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pir",
                columns: table => new
                {
                    iot_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    motion_is_detected = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pir", x => x.iot_device_id);
                    table.ForeignKey(
                        name: "FK_pir_iot_device_iot_device_id",
                        column: x => x.iot_device_id,
                        principalTable: "iot_device",
                        principalColumn: "iot_device_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "servo",
                columns: table => new
                {
                    iot_device_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    servo_current_position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servo", x => x.iot_device_id);
                    table.ForeignKey(
                        name: "FK_servo_iot_device_iot_device_id",
                        column: x => x.iot_device_id,
                        principalTable: "iot_device",
                        principalColumn: "iot_device_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_iot_device_mcu_id",
                table: "iot_device",
                column: "mcu_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ldr");

            migrationBuilder.DropTable(
                name: "light");

            migrationBuilder.DropTable(
                name: "pir");

            migrationBuilder.DropTable(
                name: "servo");

            migrationBuilder.DropTable(
                name: "iot_device");

            migrationBuilder.DropTable(
                name: "mcu");
        }
    }
}
