using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ModuleRegistration.Migrations
{
    public partial class SingularisedModuleStudentJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module_students");

            migrationBuilder.CreateTable(
                name: "module_student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ModuleId = table.Column<int>(nullable: true),
                    Uid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_module_student_module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_module_student_student_Uid",
                        column: x => x.Uid,
                        principalTable: "student",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_module_student_ModuleId",
                table: "module_student",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_module_student_Uid",
                table: "module_student",
                column: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "module_student");

            migrationBuilder.CreateTable(
                name: "module_students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ModuleId = table.Column<int>(nullable: true),
                    Uid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_module_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_module_students_module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "module",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_module_students_student_Uid",
                        column: x => x.Uid,
                        principalTable: "student",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_module_students_ModuleId",
                table: "module_students",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_module_students_Uid",
                table: "module_students",
                column: "Uid");
        }
    }
}
