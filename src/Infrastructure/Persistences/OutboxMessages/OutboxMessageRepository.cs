using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistences.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistences.OutboxMessages
{
    internal sealed class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly ShortUrlDbContext _context;

        public OutboxMessageRepository(ShortUrlDbContext context)
        {
            _context = context;
        }

        public async Task AddOutBoxMessageAsync(OutBoxMessage outBoxMessage, CancellationToken cancellationToken)
        {
            await _context.OutBoxMessages.AddAsync(outBoxMessage, cancellationToken);
        }

        public async Task DeleteOutBoxMessageAsync(Guid id, CancellationToken cancellationToken)
        {
            await _context.OutBoxMessages
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<IEnumerable<OutBoxMessage>> GetOutBoxMessagesAsync(CancellationToken cancellationToken)
        {
            var messages = await _context.OutBoxMessages
                .AsNoTracking()
                .Where(x => !x.Success)
                .ToListAsync(cancellationToken);

            return messages;
        }

        public async Task UpdateOutBoxMessageAsync(Guid Id, CancellationToken cancellationToken)
        {
            await _context.OutBoxMessages
                .Where(x => x.Id == Id)
                .ExecuteUpdateAsync(
                u => u.SetProperty(x => x.ProcessedOn, x => DateTime.UtcNow)
                .SetProperty(x => x.Success, x => true), cancellationToken);
        }

        public async Task UpdateOutBoxMessageErrorAsync(OutBoxMessage outBoxMessage, CancellationToken cancellationToken)
        {
            await _context.OutBoxMessages
                .Where(x => x.Id == outBoxMessage.Id)
                .ExecuteUpdateAsync(
                u => u.SetProperty(x => x.ProcessedOn, x => DateTime.UtcNow)
                .SetProperty(x => x.Error, x => outBoxMessage.Error), cancellationToken);
        }
    }
}