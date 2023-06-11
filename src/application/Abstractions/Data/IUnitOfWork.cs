namespace Application.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task<int> ShortUrlSaveChangesAsync(CancellationToken cancellationToken);

        Task<int> OutBoxSaveChangesAsync(CancellationToken cancellationToken);
    }
}