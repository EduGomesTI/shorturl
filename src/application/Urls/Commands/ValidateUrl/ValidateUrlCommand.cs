using MediatR;

namespace Application.Urls.Commands.ValidateUrl;

public sealed record ValidateUrlCommand(string ShortUrl) : IRequest<ValidateUrlResponse>;