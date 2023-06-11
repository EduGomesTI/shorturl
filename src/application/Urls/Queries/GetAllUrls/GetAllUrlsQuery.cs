using MediatR;

namespace Application.Urls.Queries.GetAllUrls
{
    public sealed record GetAllUrlsQuery(bool TopFive) : IRequest<IEnumerable<GetAllUrlsResponse>>
    {
    }
}