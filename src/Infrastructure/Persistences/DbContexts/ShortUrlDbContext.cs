using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.DbContexts;

public class ShortUrlDbContext : DbContext
{
    public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ShortUrl");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortUrlDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    #region DbSets

    public virtual DbSet<Url> Urls { get; set; }

    #endregion DbSets
}