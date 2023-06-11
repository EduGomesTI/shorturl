using MediatR;

namespace Application.Urls.Commands.CreateUrl
{
    public sealed record CreateUrlCommand(string OriginalUrl) : IRequest<CreateUrlResponse>;
}