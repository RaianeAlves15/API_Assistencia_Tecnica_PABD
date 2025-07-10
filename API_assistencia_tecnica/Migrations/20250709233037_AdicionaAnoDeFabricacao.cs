using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_assistencia_tecnica.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaAnoDeFabricacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FornecedorPecas",
                columns: table => new
                {
                    FornecedorId = table.Column<int>(type: "int", nullable: false),
                    PecaId = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DataUltimaCompra = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FornecedorPecas", x => new { x.FornecedorId, x.PecaId });
                    table.ForeignKey(
                        name: "FK_FornecedorPecas_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "IdFornecedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FornecedorPecas_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrcamentoPecas",
                columns: table => new
                {
                    OrcamentoId = table.Column<int>(type: "int", nullable: false),
                    PecaId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrcamentoPecas", x => new { x.OrcamentoId, x.PecaId });
                    table.ForeignKey(
                        name: "FK_OrcamentoPecas_Orcamentos_OrcamentoId",
                        column: x => x.OrcamentoId,
                        principalTable: "Orcamentos",
                        principalColumn: "IdOrcamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrcamentoPecas_Pecas_PecaId",
                        column: x => x.PecaId,
                        principalTable: "Pecas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReparoEquipamentos",
                columns: table => new
                {
                    ReparoId = table.Column<int>(type: "int", nullable: false),
                    EquipamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparoEquipamentos", x => new { x.ReparoId, x.EquipamentoId });
                    table.ForeignKey(
                        name: "FK_ReparoEquipamentos_Equipamentos_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamentos",
                        principalColumn: "IdEquipamento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparoEquipamentos_Reparos_ReparoId",
                        column: x => x.ReparoId,
                        principalTable: "Reparos",
                        principalColumn: "IdLancamentoReparo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FornecedorPecas_PecaId",
                table: "FornecedorPecas",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrcamentoPecas_PecaId",
                table: "OrcamentoPecas",
                column: "PecaId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparoEquipamentos_EquipamentoId",
                table: "ReparoEquipamentos",
                column: "EquipamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FornecedorPecas");

            migrationBuilder.DropTable(
                name: "OrcamentoPecas");

            migrationBuilder.DropTable(
                name: "ReparoEquipamentos");
        }
    }
}
