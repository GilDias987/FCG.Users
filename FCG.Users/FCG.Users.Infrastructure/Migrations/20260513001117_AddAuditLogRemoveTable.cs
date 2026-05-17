using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogRemoveTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_AUDITORIA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_AUDITORIA",
                columns: table => new
                {
                    ISN_AUDITORIA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_ACAO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ISN_ENTIDADE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CHAVES_VALORES = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VALORES_NOVOS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VALORES_ANTIGOS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DSC_TABELA = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISN_USUARIO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_AUDITORIA", x => x.ISN_AUDITORIA);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_AUDITORIA_DSC_TABELA",
                table: "TB_AUDITORIA",
                column: "DSC_TABELA");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AUDITORIA_DTH_ATUALIZACAO",
                table: "TB_AUDITORIA",
                column: "DTH_ATUALIZACAO");

            migrationBuilder.CreateIndex(
                name: "IX_TB_AUDITORIA_ISN_ENTIDADE",
                table: "TB_AUDITORIA",
                column: "ISN_ENTIDADE");
        }
    }
}
