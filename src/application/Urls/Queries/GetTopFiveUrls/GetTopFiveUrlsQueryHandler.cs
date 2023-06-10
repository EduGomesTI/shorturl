using MediatR;

namespace Application.Urls.Queries.GetTopFiveUrls;

public sealed class GetTopFiveUrlsQueryHandler : IRequestHandler<GetTopFiveUrlsQuery, IEnumerable<UrlResponse>>
{
    public Task<IEnumerable<UrlResponse>> Handle(GetTopFiveUrlsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}