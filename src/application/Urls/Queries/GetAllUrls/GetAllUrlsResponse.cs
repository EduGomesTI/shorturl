using Application.Abstractions.Responses;

namespace Application.Urls.Queries.GetAllUrls
{
    public sealed record GetAllUrlsResponse(int Id, int Hits, string OriginalUrl, string ShortUrl) : BaseResponse;
}