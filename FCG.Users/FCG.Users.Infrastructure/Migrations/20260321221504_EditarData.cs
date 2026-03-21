using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditarData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "TB_AUDITORIA",
                newName: "DTH_ATUALIZACAO");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_Timestamp",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_DTH_ATUALIZACAO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DTH_ATUALIZACAO",
                table: "TB_AUDITORIA",
                newName: "Timestamp");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_DTH_ATUALIZACAO",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_Timestamp");
        }
    }
}
