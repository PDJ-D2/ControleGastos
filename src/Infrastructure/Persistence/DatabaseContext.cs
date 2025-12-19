using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}
