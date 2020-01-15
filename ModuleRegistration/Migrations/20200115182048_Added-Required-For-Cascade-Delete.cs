using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class AddedRequiredForCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_module_ModuleId",
                table: "module_student");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student");

            migrationBuilder.AlterColumn<string>(
                name: "StudentUid",
                table: "module_student",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "module_student",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StaffUid",
                table: "module_staff",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "module_staff",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff",
                column: "StaffUid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_module_ModuleId",
                table: "module_student",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student",
                column: "StudentUid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_module_ModuleId",
                table: "module_student");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student");

            migrationBuilder.AlterColumn<string>(
                name: "StudentUid",
                table: "module_student",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "module_student",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "StaffUid",
                table: "module_staff",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "module_staff",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_module_ModuleId",
                table: "module_staff",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff",
                column: "StaffUid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_module_ModuleId",
                table: "module_student",
                column: "ModuleId",
                principalTable: "module",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_StudentUid",
                table: "module_student",
                column: "StudentUid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
