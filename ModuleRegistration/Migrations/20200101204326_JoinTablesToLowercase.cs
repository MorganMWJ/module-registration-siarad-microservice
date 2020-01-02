using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class JoinTablesToLowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStaff_module_ModuleId",
                table: "ModuleStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStaff_staff_Uid",
                table: "ModuleStaff");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudents_module_ModuleId",
                table: "ModuleStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudents_student_Uid",
                table: "ModuleStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleStudents",
                table: "ModuleStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleStaff",
                table: "ModuleStaff");

            migrationBuilder.RenameTable(
                name: "ModuleStudents",
                newName: "module_students");

            migrationBuilder.RenameTable(
                name: "ModuleStaff",
                newName: "module_staff");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStudents_Uid",
                table: "module_students",
                newName: "IX_module_students_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStudents_ModuleId",
                table: "module_students",
                newName: "IX_module_students_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStaff_Uid",
                table: "module_staff",
                newName: "IX_module_staff_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStaff_ModuleId",
                table: "module_staff",
                newName: "IX_module_staff_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_module_students",
                table: "module_students",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_module_staff",
                table: "module_staff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_Uid",
                table: "module_staff",
                column: "Uid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_students_module_ModuleId",
                table: "module_students",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_students_student_Uid",
                table: "module_students",
                column: "Uid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_Uid",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_students_module_ModuleId",
                table: "module_students");

            migrationBuilder.DropForeignKey(
                name: "FK_module_students_student_Uid",
                table: "module_students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_module_students",
                table: "module_students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_module_staff",
                table: "module_staff");

            migrationBuilder.RenameTable(
                name: "module_students",
                newName: "ModuleStudents");

            migrationBuilder.RenameTable(
                name: "module_staff",
                newName: "ModuleStaff");

            migrationBuilder.RenameIndex(
                name: "IX_module_students_Uid",
                table: "ModuleStudents",
                newName: "IX_ModuleStudents_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_module_students_ModuleId",
                table: "ModuleStudents",
                newName: "IX_ModuleStudents_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_Uid",
                table: "ModuleStaff",
                newName: "IX_ModuleStaff_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_ModuleId",
                table: "ModuleStaff",
                newName: "IX_ModuleStaff_ModuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleStudents",
                table: "ModuleStudents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleStaff",
                table: "ModuleStaff",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStaff_module_ModuleId",
                table: "ModuleStaff",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStaff_staff_Uid",
                table: "ModuleStaff",
                column: "Uid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudents_module_ModuleId",
                table: "ModuleStudents",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudents_student_Uid",
                table: "ModuleStudents",
                column: "Uid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
