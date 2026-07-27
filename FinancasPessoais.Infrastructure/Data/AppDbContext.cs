using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<ContaFinanceira> ContasFinanceiras => Set<ContaFinanceira>();
    public DbSet<Movimentacao> Movimentacoes => Set<Movimentacao>();
    public DbSet<Pagamento> Pagamentos => Set<Pagamento>();
    public DbSet<Transferencia> Transferencias => Set<Transferencia>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Anexo> Anexos => Set<Anexo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Pessoa
        modelBuilder.Entity<Pessoa>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // Categoria
        modelBuilder.Entity<Categoria>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            e.HasOne(x => x.CategoriaPai)
             .WithMany(x => x.Subcategorias)
             .HasForeignKey(x => x.CategoriaPaiId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // ContaFinanceira
        modelBuilder.Entity<ContaFinanceira>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(100);
            e.Property(x => x.SaldoInicial).HasPrecision(18, 2);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // Movimentacao
        modelBuilder.Entity<Movimentacao>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Descricao).IsRequired().HasMaxLength(200);
            e.Property(x => x.Valor).HasPrecision(18, 2);
            e.HasOne(x => x.Categoria).WithMany(x => x.Movimentacoes).HasForeignKey(x => x.CategoriaId);
            e.HasOne(x => x.Conta).WithMany(x => x.Movimentacoes).HasForeignKey(x => x.ContaId);
            e.HasOne(x => x.Pessoa).WithMany(x => x.Movimentacoes).HasForeignKey(x => x.PessoaId);
            e.HasOne(x => x.TransferenciaRelacionada)
             .WithOne()
             .HasForeignKey<Movimentacao>(x => x.TransferenciaRelacionadaId)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // Pagamento
        modelBuilder.Entity<Pagamento>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Valor).HasPrecision(18, 2);
            e.Property(x => x.Juros).HasPrecision(18, 2);
            e.Property(x => x.Multa).HasPrecision(18, 2);
            e.Property(x => x.Desconto).HasPrecision(18, 2);
            e.HasOne(x => x.Movimentacao).WithMany(x => x.Pagamentos).HasForeignKey(x => x.MovimentacaoId);
            e.HasOne(x => x.Conta).WithMany().HasForeignKey(x => x.ContaId);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // Transferencia
        modelBuilder.Entity<Transferencia>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Valor).HasPrecision(18, 2);
            e.Property(x => x.Tarifa).HasPrecision(18, 2);
            e.HasOne(x => x.ContaOrigem).WithMany().HasForeignKey(x => x.ContaOrigemId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.ContaDestino).WithMany().HasForeignKey(x => x.ContaDestinoId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.MovimentacaoSaida).WithOne().HasForeignKey<Transferencia>(x => x.MovimentacaoSaidaId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(x => x.MovimentacaoEntrada).WithOne().HasForeignKey<Transferencia>(x => x.MovimentacaoEntradaId).OnDelete(DeleteBehavior.SetNull);
            e.HasQueryFilter(x => x.ExcluidoEm == null);
        });

        // Tag
        modelBuilder.Entity<Tag>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Nome).IsRequired().HasMaxLength(50);
        });

        // Anexo
        modelBuilder.Entity<Anexo>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.NomeArquivo).IsRequired().HasMaxLength(255);
            e.Property(x => x.CaminhoInterno).IsRequired().HasMaxLength(500);
        });

        // Seed de categorias padrão — GUIDs fixos obrigatórios para HasData
        var alimentacao = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var transporte = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var salario = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var moradia = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var saude = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var lazer = Guid.Parse("66666666-6666-6666-6666-666666666666");

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria { Id = alimentacao, Nome = "Alimentação", Cor = "#FF5733", Icone = "🍔", TipoPadrao = TipoMovimentacao.Despesa, CriadoEm = new DateTime(2026, 1, 1) },
            new Categoria { Id = transporte, Nome = "Transporte", Cor = "#33FF57", Icone = "🚗", TipoPadrao = TipoMovimentacao.Despesa, CriadoEm = new DateTime(2026, 1, 1) },
            new Categoria { Id = salario, Nome = "Salário", Cor = "#3357FF", Icone = "💰", TipoPadrao = TipoMovimentacao.Receita, CriadoEm = new DateTime(2026, 1, 1) },
            new Categoria { Id = moradia, Nome = "Moradia", Cor = "#FF33F6", Icone = "🏠", TipoPadrao = TipoMovimentacao.Despesa, CriadoEm = new DateTime(2026, 1, 1) },
            new Categoria { Id = saude, Nome = "Saúde", Cor = "#FF3333", Icone = "🏥", TipoPadrao = TipoMovimentacao.Despesa, CriadoEm = new DateTime(2026, 1, 1) },
            new Categoria { Id = lazer, Nome = "Lazer", Cor = "#33FFF6", Icone = "🎮", TipoPadrao = TipoMovimentacao.Despesa, CriadoEm = new DateTime(2026, 1, 1) }
        );
    }
}