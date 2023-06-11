using Application.Abstractions.Data;
using Infrastructure.Persistences.DbContexts;

namespace Infrastructure.Persistences
{
    internal sealed class UnitOfWOrk : IUnitOfWork
    {
        private readonly ShortUrlDbContext _shortUrlDbContext;
        private readonly OutboxDbContext _outboxDbContext;

        public UnitOfWOrk(ShortUrlDbContext shortUrlDbContext, OutboxDbContext outboxDbContext)
        {
            _shortUrlDbContext = shortUrlDbContext;
            _outboxDbContext = outboxDbContext;
        }

        public async Task<int> OutBoxSaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _outboxDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> ShortUrlSaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _shortUrlDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}