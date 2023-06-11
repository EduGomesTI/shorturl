using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistences.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.Urls
{
    internal sealed class UrlRepository : IUrlRepository
    {
        private readonly ShortUrlDbContext _context;

        public UrlRepository(ShortUrlDbContext context)
        {
            _context = context;
        }

        public async Task AddUrlAsync(Url url, CancellationToken cancellationToken)
        {
            await _context.AddAsync(url, cancellationToken);
        }

        public async Task AppendHitsAsync(Url url, CancellationToken cancellationToken)
        {
            await _context.Urls
                .Where(u => u.Id == url.Id)
                .ExecuteUpdateAsync(
                u => u.SetProperty(x => x.Hits, x => x.Hits + 1), cancellationToken);
        }

        public async Task<IEnumerable<Url>> GetAllUrlsAsync(CancellationToken cancellationToken)
        {
            var result = await _context
                .Urls
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Url> GetOriginalUrlAsync(string shortUrl, CancellationToken cancellationToken)
        {
            var result = await _context
                                .Urls
                                .AsNoTracking()
                                .Where(x => x.ShortUrl == shortUrl)
                                .FirstOrDefaultAsync(cancellationToken);

            return result!;
        }

        public async Task<IEnumerable<Url>> GetTopFiveUrlsAsync(CancellationToken cancellationToken)
        {
            return await _context.Urls
                .AsNoTracking()
                .OrderByDescending(x => x.Hits)
                .Take(5)
                .ToListAsync(cancellationToken);
        }
    }
}