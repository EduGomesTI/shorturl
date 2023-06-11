using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOutboxMessageRepository
    {
        Task AddOutBoxMessageAsync(OutBoxMessage outBoxMessage, CancellationToken cancellationToken);

        Task<IEnumerable<OutBoxMessage>> GetOutBoxMessagesAsync(CancellationToken cancellationToken);

        Task UpdateOutBoxMessageAsync(Guid Id, CancellationToken cancellationToken);

        Task UpdateOutBoxMessageErrorAsync(OutBoxMessage outBoxMessage, CancellationToken cancellationToken);

        Task DeleteOutBoxMessageAsync(Guid id, CancellationToken cancellationToken);
    }
}