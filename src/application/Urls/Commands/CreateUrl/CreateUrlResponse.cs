using Application.Abstractions.Responses;
using Domain.Entities;

namespace Application.Urls.Commands.CreateUrl;

public sealed record CreateUrlResponse(DateTime CreateDate, string OriginalUrl, string ShortUrl) : BaseResponse
{
    public static implicit operator CreateUrlResponse(Url url) => new(
        url.CreateDate,
        url.OriginalUrl,
        url.ShortUrl
        );
}