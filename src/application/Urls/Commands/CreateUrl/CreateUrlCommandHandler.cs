using Application.Abstractions.Data;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Urls.Commands.CreateUrl
{
    internal sealed class CreateUrlCommandHandler : IRequestHandler<CreateUrlCommand, UrlResponse>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUnitOfWork _shortUrlUnitOfWOrk;
        private readonly ILogger<CreateUrlCommandHandler> _logger;

        public CreateUrlCommandHandler(IUrlRepository urlRepository, IUnitOfWork shortUrlUnitOfWOrk, ILogger<CreateUrlCommandHandler> logger)
        {
            _urlRepository = urlRepository;
            _shortUrlUnitOfWOrk = shortUrlUnitOfWOrk;
            _logger = logger;
        }

        async Task<UrlResponse> IRequestHandler<CreateUrlCommand, UrlResponse>.Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            Url url = new(request.OriginalUrl);

            url.CreateShortUrl();

            _logger.LogInformation($"Short url criada: https://{url.ShortUrl}");

            _logger.LogInformation("Persiste dados no Postgres.");
            var result = await _urlRepository.AddUrlAsync(url, cancellationToken);
            await _shortUrlUnitOfWOrk.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Persiste dados no banco Outbox para mensageria.");

            return result;
        }
    }
}