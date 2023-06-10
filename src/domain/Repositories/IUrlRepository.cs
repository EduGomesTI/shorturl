using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUrlRepository
    {
        Task<Url> AddUrlAsync(Url url, CancellationToken cancellationToken);
    }
}