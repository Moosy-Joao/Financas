using FinancasPessoais.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

// Ponto de entrada para dotnet ef usar o DbContext
var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlite("Data Source=temp.db");

using var context = new AppDbContext(optionsBuilder.Options);
Console.WriteLine("DbContext criado para migrations.");