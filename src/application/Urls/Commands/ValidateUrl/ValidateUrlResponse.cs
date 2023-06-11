using Application.Abstractions.Responses;

namespace Application.Urls.Commands.ValidateUrl
{
    public record ValidateUrlResponse(string OriginalUrl, string ShortUrl) : BaseResponse;
}