using Microsoft.EntityFrameworkCore.Migrations;

namespace ModuleRegistration.Migrations
{
    public partial class AllowStaffNameToBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "surname",
                table: "staff",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "forename",
                table: "staff",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "surname",
                table: "staff",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "forename",
                table: "staff",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
