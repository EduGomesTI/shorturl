using Application.Abstractions.Data;
using Application.Services;
using Application.Urls.Commands.CallOriginalUrl;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Urls.Commands.CreateUrl
{
    internal sealed class CreateUrlCommandHandler : IRequestHandler<CreateUrlCommand, CreateUrlResponse>
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IUnitOfWork _shortUrlUnitOfWOrk;
        private readonly ILogger<CreateUrlCommandHandler> _logger;
        private readonly IValidator<CreateUrlCommand> _validator;

        public CreateUrlCommandHandler(IUrlRepository urlRepository,
            IUnitOfWork shortUrlUnitOfWOrk,
            ILogger<CreateUrlCommandHandler> logger,
            IValidator<CreateUrlCommand> validator)
        {
            _urlRepository = urlRepository;
            _shortUrlUnitOfWOrk = shortUrlUnitOfWOrk;
            _logger = logger;
            _validator = validator;
        }

        async Task<CreateUrlResponse> IRequestHandler<CreateUrlCommand, CreateUrlResponse>.Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validar request");
            var validate = await _validator.ValidateAsync(request, opt =>
            {
                opt.IncludeRuleSets("Add");
            }, cancellationToken);

            if(!validate.IsValid)
            {
                _logger.LogError("Houveram erros de validação.");
                CreateUrlResponse response = new(DateTime.UtcNow, string.Empty, string.Empty);
                response.AddMessages(ConvertErrorsService.GetError(validate.Errors));
                return response;
            }

            Url url = new(request.OriginalUrl);

            url.CreateShortUrl();

            _logger.LogInformation($"Short url criada: https://{url.ShortUrl}");

            _logger.LogInformation("Persiste dados no Postgres.");
            await _urlRepository.AddUrlAsync(url, cancellationToken);
            await _shortUrlUnitOfWOrk.ShortUrlSaveChangesAsync(cancellationToken);

            _logger.LogInformation("Persiste dados no banco Outbox para mensageria.");

            return url;
        }
    }
}