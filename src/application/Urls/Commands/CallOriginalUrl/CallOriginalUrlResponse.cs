using Application.Abstractions.Responses;

namespace Application.Urls.Commands.CallOriginalUrl;

public record CallOriginalUrlResponse(string OriginalUrl) : BaseResponse;