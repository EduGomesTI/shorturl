using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Persistences.Urls
{
    internal class UrlRepository : IUrlRepository
    {
        public async Task<Url> AddUrlAsync(Url url, CancellationToken cancellationToken)
        {
            var result = new Url(1, 5, url.OriginalUrl, url.ShortUrl);

            return result;
        }
    }
}