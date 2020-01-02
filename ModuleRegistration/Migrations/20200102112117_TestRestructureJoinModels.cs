using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class TestRestructureJoinModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_student_uid",
                table: "module_student");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "module_student",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "student_uid",
                table: "module_student",
                newName: "StudentUid");

            migrationBuilder.RenameIndex(
                name: "IX_module_student_student_uid",
                table: "module_student",
                newName: "IX_module_student_StudentUid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student",
                column: "StudentUid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "module_student",
                newName: "module_id");

            migrationBuilder.RenameColumn(
                name: "StudentUid",
                table: "module_student",
                newName: "student_uid");

            migrationBuilder.RenameIndex(
                name: "IX_module_student_StudentUid",
                table: "module_student",
                newName: "IX_module_student_student_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_student_uid",
                table: "module_student",
                column: "student_uid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
