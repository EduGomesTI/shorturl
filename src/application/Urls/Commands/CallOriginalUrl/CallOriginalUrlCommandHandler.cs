using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Urls.Commands.CallOriginalUrl;

internal sealed class CallOriginalUrlCommandHandler : IRequestHandler<CallOriginalUrlCommand, CallOriginalUrlResponse>
{
    private readonly IUrlRepository _urlRepository;
    private readonly ILogger<CallOriginalUrlCommandHandler> _logger;
    private readonly IValidator<CallOriginalUrlCommand> _validator;

    public CallOriginalUrlCommandHandler(IUrlRepository urlRepository,
        ILogger<CallOriginalUrlCommandHandler> logger,
        IValidator<CallOriginalUrlCommand> validator)
    {
        _urlRepository = urlRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<CallOriginalUrlResponse> Handle(CallOriginalUrlCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validar request");
        var validate = await _validator.ValidateAsync(request, opt =>
        {
            opt.IncludeRuleSets("Add");
        }, cancellationToken);

        if(!validate.IsValid)
        {
            _logger.LogError("Houveram erros de validação.");
            CallOriginalUrlResponse response = new(string.Empty);
            response.AddMessages(ConvertErrorsService.GetError(validate.Errors));
            return response;
        }

        Url url = new();

        url.AddShortUrl(request.ShortUrl);

        _logger.LogInformation("Retorna objeto no banco com esta short url.");
        var newUrl = await _urlRepository.GetOriginalUrlAsync(url.ShortUrl, cancellationToken);

        if(newUrl is null)
        {
            var errorMessage = $"{request.ShortUrl} não tem nenhuma url associada a ela.";

            _logger.LogError(errorMessage);
            CallOriginalUrlResponse resultError = new(string.Empty);
            resultError.AddMessage(errorMessage);
            return resultError;
        }

        _logger.LogInformation("Adiciona um acesso ao link.");
        await _urlRepository.AppendHitsAsync(newUrl, cancellationToken);

        var result = new CallOriginalUrlResponse(newUrl.OriginalUrl);

        return result;
    }
}