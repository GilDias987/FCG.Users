using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class edtmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passoword",
                table: "TB_USUARIO");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "TB_USUARIO",
                newName: "DSC_EMAIL");

            migrationBuilder.AlterColumn<string>(
                name: "DSC_EMAIL",
                table: "TB_USUARIO",
                type: "VARCHAR(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DSC_NOME",
                table: "TB_USUARIO",
                type: "VARCHAR(1000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DSC_NOME",
                table: "TB_USUARIO");

            migrationBuilder.RenameColumn(
                name: "DSC_EMAIL",
                table: "TB_USUARIO",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "TB_USUARIO",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1000)");

            migrationBuilder.AddColumn<string>(
                name: "Passoword",
                table: "TB_USUARIO",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
