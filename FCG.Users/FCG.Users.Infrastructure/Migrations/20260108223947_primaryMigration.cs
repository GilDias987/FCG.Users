using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FCG.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class primaryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GRUPO_USUARIO",
                columns: table => new
                {
                    ISN_GRUPO_USUARIO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_GRUPO = table.Column<string>(type: "VARCHAR(500)", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GRUPO_USUARIO", x => x.ISN_GRUPO_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "TB_USUARIO",
                columns: table => new
                {
                    ISN_USUARIO = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DSC_SENHA = table.Column<string>(type: "VARCHAR(1000)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passoword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISN_GRUPO_USUARIO = table.Column<int>(type: "INT", nullable: false),
                    DTH_CRIACAO = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DTH_ATUALIZACAO = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USUARIO", x => x.ISN_USUARIO);
                    table.ForeignKey(
                        name: "FK_TB_USUARIO_TB_GRUPO_USUARIO_ISN_GRUPO_USUARIO",
                        column: x => x.ISN_GRUPO_USUARIO,
                        principalTable: "TB_GRUPO_USUARIO",
                        principalColumn: "ISN_GRUPO_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_ISN_GRUPO_USUARIO",
                table: "TB_USUARIO",
                column: "ISN_GRUPO_USUARIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USUARIO");

            migrationBuilder.DropTable(
                name: "TB_GRUPO_USUARIO");
        }
    }
}
