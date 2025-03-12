using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class GestionMenuDbContext : IdentityDbContext<Usuario>
{
    public GestionMenuDbContext(DbContextOptions<GestionMenuDbContext> options) : base(options) { }

    public DbSet<Plato> Platos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Categoria>()
            .HasMany(c => c.Platos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId);
    }
}

