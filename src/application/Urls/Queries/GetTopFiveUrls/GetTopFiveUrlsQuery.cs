using MediatR;

namespace Application.Urls.Queries.GetTopFiveUrls;

public sealed record GetTopFiveUrlsQuery : IRequest<IEnumerable<UrlResponse>>;