using Domain.Entities;

namespace Application.Urls;

public record UrlResponse(int Id, DateTime CreateDate, int Hits, string OriginalUrl, string ShortUrl)
{
    public static implicit operator UrlResponse(Url url) => new(
        url.Id,
        url.CreateDate,
        url.Hits,
        url.OriginalUrl,
        url.ShortUrl
        );
}