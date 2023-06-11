using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUrlRepository
    {
        Task AddUrlAsync(Url url, CancellationToken cancellationToken);

        Task<Url> GetOriginalUrlAsync(string shortUrl, CancellationToken cancellationToken);

        Task AppendHitsAsync(Url url, CancellationToken cancellationToken);

        Task<IEnumerable<Url>> GetAllUrlsAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Url>> GetTopFiveUrlsAsync(CancellationToken cancellationToken);
    }
}