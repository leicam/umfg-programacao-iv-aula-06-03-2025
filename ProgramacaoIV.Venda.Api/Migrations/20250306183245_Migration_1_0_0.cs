using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgramacaoIV.Venda.Api.Migrations
{
    /// <inheritdoc />
    public partial class Migration_1_0_0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    NM_CLIENTE = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    NR_CPF = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    DS_ENDERECO = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    IN_ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PRODUTO",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "char(36)", nullable: false),
                    NR_EAN = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    DS_PRODUTO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    VL_PRECO_COMPRA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VL_PRECO_VENDA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QT_ESTOQUE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IN_ATIVO = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DT_CRIACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DT_ATUALIZACAO = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUTO", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "PRODUTO");
        }
    }
}
