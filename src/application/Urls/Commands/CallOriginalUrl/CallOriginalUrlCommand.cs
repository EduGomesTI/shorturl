using MediatR;

namespace Application.Urls.Commands.CallOriginalUrl;

public sealed record CallOriginalUrlCommand(string ShortUrl) : IRequest<CallOriginalUrlResponse>;