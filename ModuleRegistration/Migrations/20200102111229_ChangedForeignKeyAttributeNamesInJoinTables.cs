using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class ChangedForeignKeyAttributeNamesInJoinTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_Uid",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_Uid",
                table: "module_student");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "module_student",
                newName: "student_uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "module_student",
                newName: "module_id");

            migrationBuilder.RenameIndex(
                name: "IX_module_student_Uid",
                table: "module_student",
                newName: "IX_module_student_student_uid");

            migrationBuilder.RenameColumn(
                name: "Uid",
                table: "module_staff",
                newName: "staff_uid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "module_staff",
                newName: "module_id");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_Uid",
                table: "module_staff",
                newName: "IX_module_staff_staff_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_staff_uid",
                table: "module_staff",
                column: "staff_uid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_student_uid",
                table: "module_student",
                column: "student_uid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_staff_uid",
                table: "module_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_module_student_student_student_uid",
                table: "module_student");

            migrationBuilder.RenameColumn(
                name: "student_uid",
                table: "module_student",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "module_student",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_module_student_student_uid",
                table: "module_student",
                newName: "IX_module_student_Uid");

            migrationBuilder.RenameColumn(
                name: "staff_uid",
                table: "module_staff",
                newName: "Uid");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "module_staff",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_staff_uid",
                table: "module_staff",
                newName: "IX_module_staff_Uid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_Uid",
                table: "module_staff",
                column: "Uid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_module_student_student_Uid",
                table: "module_student",
                column: "Uid",
                principalTable: "student",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
