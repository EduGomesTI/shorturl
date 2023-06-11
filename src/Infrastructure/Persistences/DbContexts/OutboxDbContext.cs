using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.DbContexts;

public class OutboxDbContext : DbContext
{
    public OutboxDbContext(DbContextOptions<OutboxDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Outbox");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OutboxDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    #region DbSets

    public virtual DbSet<Url> Urls { get; set; }

    #endregion DbSets
}