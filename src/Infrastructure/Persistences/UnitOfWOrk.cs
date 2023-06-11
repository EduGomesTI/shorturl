using Application.Abstractions.Data;
using Infrastructure.Persistences.DbContexts;

namespace Infrastructure.Persistences
{
    internal sealed class UnitOfWOrk : IUnitOfWork
    {
        private readonly ShortUrlDbContext _shortUrlDbContext;

        public UnitOfWOrk(ShortUrlDbContext shortUrlDbContext)
        {
            _shortUrlDbContext = shortUrlDbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _shortUrlDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}