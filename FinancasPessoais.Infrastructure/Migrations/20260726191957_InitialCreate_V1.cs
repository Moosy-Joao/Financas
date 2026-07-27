using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinancasPessoais.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cor = table.Column<string>(type: "TEXT", nullable: true),
                    Icone = table.Column<string>(type: "TEXT", nullable: true),
                    TipoPadrao = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaPaiId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categorias_Categorias_CategoriaPaiId",
                        column: x => x.CategoriaPaiId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContasFinanceiras",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Instituicao = table.Column<string>(type: "TEXT", nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    Arquivado = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasFinanceiras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Competencia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Vencimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Situacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    CategoriaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PessoaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    TransferenciaRelacionadaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_ContasFinanceiras_ContaId",
                        column: x => x.ContaId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Movimentacoes_TransferenciaRelacionadaId",
                        column: x => x.TransferenciaRelacionadaId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimentacoes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovimentacaoTag",
                columns: table => new
                {
                    MovimentacoesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoTag", x => new { x.MovimentacoesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MovimentacaoTag_Movimentacoes_MovimentacoesId",
                        column: x => x.MovimentacoesId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacaoTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Juros = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Multa = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Desconto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    DataPagamento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    MovimentacaoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_ContasFinanceiras_ContaId",
                        column: x => x.ContaId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Movimentacoes_MovimentacaoId",
                        column: x => x.MovimentacaoId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Tarifa = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    ContaOrigemId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaDestinoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MovimentacaoSaidaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    MovimentacaoEntradaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transferencias_ContasFinanceiras_ContaDestinoId",
                        column: x => x.ContaDestinoId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferencias_ContasFinanceiras_ContaOrigemId",
                        column: x => x.ContaOrigemId,
                        principalTable: "ContasFinanceiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transferencias_Movimentacoes_MovimentacaoEntradaId",
                        column: x => x.MovimentacaoEntradaId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Transferencias_Movimentacoes_MovimentacaoSaidaId",
                        column: x => x.MovimentacaoSaidaId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Anexos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomeArquivo = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CaminhoInterno = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    TipoMime = table.Column<string>(type: "TEXT", nullable: true),
                    TamanhoBytes = table.Column<long>(type: "INTEGER", nullable: false),
                    MovimentacaoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PagamentoId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ExcluidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Origem = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anexos_Movimentacoes_MovimentacaoId",
                        column: x => x.MovimentacaoId,
                        principalTable: "Movimentacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Anexos_Pagamentos_PagamentoId",
                        column: x => x.PagamentoId,
                        principalTable: "Pagamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "AtualizadoEm", "CategoriaPaiId", "Cor", "CriadoEm", "ExcluidoEm", "Icone", "Nome", "Origem", "TipoPadrao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, null, "#FF5733", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🍔", "Alimentação", "Manual", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, null, "#33FF57", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🚗", "Transporte", "Manual", 1 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, null, "#3357FF", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "💰", "Salário", "Manual", 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, null, "#FF33F6", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🏠", "Moradia", "Manual", 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, null, "#FF3333", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🏥", "Saúde", "Manual", 1 },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, null, "#33FFF6", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "🎮", "Lazer", "Manual", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_MovimentacaoId",
                table: "Anexos",
                column: "MovimentacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexos_PagamentoId",
                table: "Anexos",
                column: "PagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_CategoriaPaiId",
                table: "Categorias",
                column: "CategoriaPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoTag_TagsId",
                table: "MovimentacaoTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_CategoriaId",
                table: "Movimentacoes",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ContaId",
                table: "Movimentacoes",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_PessoaId",
                table: "Movimentacoes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_TransferenciaRelacionadaId",
                table: "Movimentacoes",
                column: "TransferenciaRelacionadaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_ContaId",
                table: "Pagamentos",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_MovimentacaoId",
                table: "Pagamentos",
                column: "MovimentacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_ContaDestinoId",
                table: "Transferencias",
                column: "ContaDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_ContaOrigemId",
                table: "Transferencias",
                column: "ContaOrigemId");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_MovimentacaoEntradaId",
                table: "Transferencias",
                column: "MovimentacaoEntradaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_MovimentacaoSaidaId",
                table: "Transferencias",
                column: "MovimentacaoSaidaId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anexos");

            migrationBuilder.DropTable(
                name: "MovimentacaoTag");

            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Movimentacoes");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "ContasFinanceiras");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
