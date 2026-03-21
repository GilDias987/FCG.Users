using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TB_AUDITORIA",
                newName: "ISN_USUARIO");

            migrationBuilder.RenameColumn(
                name: "TableName",
                table: "TB_AUDITORIA",
                newName: "DSC_TABELA");

            migrationBuilder.RenameColumn(
                name: "OldValues",
                table: "TB_AUDITORIA",
                newName: "VALORES_ANTIGOS");

            migrationBuilder.RenameColumn(
                name: "NewValues",
                table: "TB_AUDITORIA",
                newName: "VALORES_NOVOS");

            migrationBuilder.RenameColumn(
                name: "KeyValues",
                table: "TB_AUDITORIA",
                newName: "CHAVES_VALORES");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "TB_AUDITORIA",
                newName: "ISN_ENTIDADE");

            migrationBuilder.RenameColumn(
                name: "Action",
                table: "TB_AUDITORIA",
                newName: "DSC_ACAO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_AUDITORIA",
                newName: "ISN_AUDITORIA");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_TableName",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_DSC_TABELA");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_EntityId",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_ISN_ENTIDADE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VALORES_NOVOS",
                table: "TB_AUDITORIA",
                newName: "NewValues");

            migrationBuilder.RenameColumn(
                name: "VALORES_ANTIGOS",
                table: "TB_AUDITORIA",
                newName: "OldValues");

            migrationBuilder.RenameColumn(
                name: "ISN_USUARIO",
                table: "TB_AUDITORIA",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ISN_ENTIDADE",
                table: "TB_AUDITORIA",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "DSC_TABELA",
                table: "TB_AUDITORIA",
                newName: "TableName");

            migrationBuilder.RenameColumn(
                name: "DSC_ACAO",
                table: "TB_AUDITORIA",
                newName: "Action");

            migrationBuilder.RenameColumn(
                name: "CHAVES_VALORES",
                table: "TB_AUDITORIA",
                newName: "KeyValues");

            migrationBuilder.RenameColumn(
                name: "ISN_AUDITORIA",
                table: "TB_AUDITORIA",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_ISN_ENTIDADE",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_TB_AUDITORIA_DSC_TABELA",
                table: "TB_AUDITORIA",
                newName: "IX_TB_AUDITORIA_TableName");
        }
    }
}
