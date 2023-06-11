using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Urls.Queries.GetAllUrls
{
    internal class GetAllUrlsQueryHandler : IRequestHandler<GetAllUrlsQuery, IEnumerable<GetAllUrlsResponse>>
    {
        private readonly ILogger<GetAllUrlsQueryHandler> _logger;
        private readonly IUrlRepository _urlRepository;

        public GetAllUrlsQueryHandler(ILogger<GetAllUrlsQueryHandler> logger, IUrlRepository urlRepository)
        {
            _logger = logger;
            _urlRepository = urlRepository;
        }

        public async Task<IEnumerable<GetAllUrlsResponse>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
        {
            List<Url> urls = new();

            if(request.TopFive)
            {
                _logger.LogInformation("Retorna os 5 links com mais acessos.");
                urls.AddRange(await _urlRepository.GetTopFiveUrlsAsync(cancellationToken));
            }

            if(!request.TopFive)
            {
                _logger.LogInformation("Retorna todos os objetos do banco");
                urls.AddRange(await _urlRepository.GetAllUrlsAsync(cancellationToken));
            }

            List<GetAllUrlsResponse> getAllUrlsResponses = new();

            foreach(var url in urls)
            {
                getAllUrlsResponses.Add(new GetAllUrlsResponse(url.Id, url.Hits, url.OriginalUrl, url.ShortUrl));
            }

            return getAllUrlsResponses;
        }
    }
}