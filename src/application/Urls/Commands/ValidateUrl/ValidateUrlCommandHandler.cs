using System.Net;
using Application.Services;
using Application.Urls.Commands.CreateUrl;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Urls.Commands.ValidateUrl;

internal sealed class ValidateUrlCommandHandler : IRequestHandler<ValidateUrlCommand, ValidateUrlResponse>
{
    private readonly IUrlRepository _urlRepository;
    private readonly ILogger<ValidateUrlCommandHandler> _logger;
    private readonly IValidator<ValidateUrlCommand> _validator;

    public ValidateUrlCommandHandler(IUrlRepository urlRepository,
        ILogger<ValidateUrlCommandHandler> logger,
        IValidator<ValidateUrlCommand> validator)
    {
        _urlRepository = urlRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<ValidateUrlResponse> Handle(ValidateUrlCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validar request");
        var validate = await _validator.ValidateAsync(request, opt =>
        {
            opt.IncludeRuleSets("Add");
        }, cancellationToken);

        if(!validate.IsValid)
        {
            _logger.LogError("Houveram erros de validação.");
            ValidateUrlResponse response = new(string.Empty, string.Empty);
            response.AddMessages(ConvertErrorsService.GetError(validate.Errors));
            return response;
        }

        Url url = new();

        _logger.LogInformation($"Limpa e adiciona {request.ShortUrl} para o objeto url.");
        url.AddShortUrl(request.ShortUrl);
        _logger.LogInformation($"Novo shortUrl: {url.ShortUrl}.");

        _logger.LogInformation("Retorna objeto no banco com esta short url.");
        var newUrl = await _urlRepository.GetOriginalUrlAsync(url.ShortUrl, cancellationToken);

        if(newUrl is null)
        {
            var errorMessage = $"{request.ShortUrl} não tem nenhuma url associada a ela.";

            _logger.LogError(errorMessage);
            var resultError = new ValidateUrlResponse(string.Empty, string.Empty);
            resultError.AddMessage(errorMessage);
            return resultError;
        }

        var result = new ValidateUrlResponse(newUrl.OriginalUrl, newUrl.ShortUrl);

        _logger.LogInformation("Testa se a url ainda é válida.");
        HttpResponseMessage clientResponse = new();
        HttpClient client = new();
        try
        {
            clientResponse = await client.GetAsync(newUrl.OriginalUrl, cancellationToken);
        }
        catch(HttpRequestException ex)
        {

            result.AddMessage($"{newUrl.OriginalUrl} retornou a seguinte mensagem: {ex.Message}");
            clientResponse.StatusCode = HttpStatusCode.NotFound;
        }
        catch(UriFormatException ex)
        {
            result.AddMessage($"{newUrl.OriginalUrl} retornou a seguinte mensagem: {ex.Message}");
            clientResponse.StatusCode = HttpStatusCode.BadRequest;
        }
        catch(Exception ex)
        {
            result.AddMessage($"{newUrl.OriginalUrl} retornou a seguinte mensagem: {ex.Message}");
            clientResponse.StatusCode = HttpStatusCode.BadRequest;
        }      

        result.AddMessage($"{newUrl.OriginalUrl} retornou o seguinte status: {clientResponse.StatusCode}");
        return result;
    }
}