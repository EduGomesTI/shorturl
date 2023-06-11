using Application.Abstractions.Data;
using Application.Services;
using Application.Urls.Commands.CallOriginalUrl;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public CreateUrlCommandHandler(IUrlRepository urlRepository,
            IUnitOfWork shortUrlUnitOfWOrk,
            ILogger<CreateUrlCommandHandler> logger,
            IValidator<CreateUrlCommand> validator,
            IOutboxMessageRepository outboxMessageRepository)
        {
            _urlRepository = urlRepository;
            _shortUrlUnitOfWOrk = shortUrlUnitOfWOrk;
            _logger = logger;
            _validator = validator;
            _outboxMessageRepository = outboxMessageRepository;
        }

        async Task<CreateUrlResponse> IRequestHandler<CreateUrlCommand, CreateUrlResponse>.Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Validar request");
            ValidationResult validate = await ValidateRequest(request, cancellationToken);

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

            OutBoxMessage outBoxMessage = CreateOutBoxMessage.Handler(url);

            await _outboxMessageRepository.AddOutBoxMessageAsync(outBoxMessage, cancellationToken);

            await _shortUrlUnitOfWOrk.SaveChangesAsync(cancellationToken);

            return url;
        }

        private async Task<ValidationResult> ValidateRequest(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            return await _validator.ValidateAsync(request, opt =>
            {
                opt.IncludeRuleSets("Add");
            }, cancellationToken);
        }
    }
}