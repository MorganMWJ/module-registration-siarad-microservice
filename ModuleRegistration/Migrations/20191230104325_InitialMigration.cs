using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ModuleRegistration.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "module",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    code = table.Column<string>(nullable: false),
                    year = table.Column<string>(nullable: false),
                    class_code = table.Column<string>(nullable: false),
                    coordinator_uid = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    uid = table.Column<string>(nullable: false),
                    forename = table.Column<string>(nullable: false),
                    surname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staff", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    uid = table.Column<string>(nullable: false),
                    forename = table.Column<string>(nullable: false),
                    surname = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "ModuleStaff",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ModuleId = table.Column<int>(nullable: true),
                    Uid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleStaff_module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleStaff_staff_Uid",
                        column: x => x.Uid,
                        principalTable: "staff",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleStudents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ModuleId = table.Column<int>(nullable: true),
                    Uid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStudents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleStudents_module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleStudents_student_Uid",
                        column: x => x.Uid,
                        principalTable: "student",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStaff_ModuleId",
                table: "ModuleStaff",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStaff_Uid",
                table: "ModuleStaff",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStudents_ModuleId",
                table: "ModuleStudents",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStudents_Uid",
                table: "ModuleStudents",
                column: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleStaff");

            migrationBuilder.DropTable(
                name: "ModuleStudents");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "module");

            migrationBuilder.DropTable(
                name: "student");
        }
    }
}
