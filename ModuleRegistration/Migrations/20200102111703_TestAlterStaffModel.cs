using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class TestAlterStaffModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_staff_uid",
                table: "module_staff");

            migrationBuilder.RenameColumn(
                name: "module_id",
                table: "module_staff",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "staff_uid",
                table: "module_staff",
                newName: "StaffUid");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_staff_uid",
                table: "module_staff",
                newName: "IX_module_staff_StaffUid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff",
                column: "StaffUid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_module_staff_staff_StaffUid",
                table: "module_staff");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "module_staff",
                newName: "module_id");

            migrationBuilder.RenameColumn(
                name: "StaffUid",
                table: "module_staff",
                newName: "staff_uid");

            migrationBuilder.RenameIndex(
                name: "IX_module_staff_StaffUid",
                table: "module_staff",
                newName: "IX_module_staff_staff_uid");

            migrationBuilder.AddForeignKey(
                name: "FK_module_staff_staff_staff_uid",
                table: "module_staff",
                column: "staff_uid",
                principalTable: "staff",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
