using System.Diagnostics;
using Application.Urls.Commands.CallOriginalUrl;
using Application.Urls.Commands.CreateUrl;
using Application.Urls.Commands.ValidateUrl;
using Application.Urls.Queries.GetAllUrls;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.EndPoints
{
    public sealed class UrlEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", async (string id, ISender sender, CancellationToken cancellationToken) =>
            {
                CallOriginalUrlCommand callOriginalUrlQuery = new(id);

                var result = await sender.Send(callOriginalUrlQuery, cancellationToken);

                if(result.Messages.Any())
                    return Results.BadRequest(result.Messages);

                Process.Start(new ProcessStartInfo { FileName = result.OriginalUrl, UseShellExecute = true });

                return Results.Ok();
            });

            app.MapPost("/url", async (CreateUrlCommand request, HttpContext context, ISender sender) =>
            {
                var createUrlResponse = await sender.Send(request);

                if(createUrlResponse.Messages.Any())
                {
                    return Results.BadRequest(createUrlResponse.Messages);
                }

                var uri = $"{GetApiUrl(context)}/{createUrlResponse.ShortUrl}";

                var result = createUrlResponse with { ShortUrl = uri };

                return Results.Ok(result);
            });

            app.MapGet("/url/getAll", async (ISender sender, CancellationToken cancellationToken) =>
            {
                GetAllUrlsQuery request = new(false);

                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            });

            app.MapGet("/url/topFive", async (ISender sender, CancellationToken cancellationToken) =>
            {
                GetAllUrlsQuery request = new(true);

                var result = await sender.Send(request, cancellationToken);

                return Results.Ok(result);
            });

            app.MapPost("/url/validate", async (ValidateUrlCommand request, HttpContext context, ISender sender) =>
            {
                var host = GetApiUrl(context);

                ValidateUrlCommand newRequest = request with { ShortUrl = request.ShortUrl.Replace($"{host}/", "") };

                ValidateUrlResponse validateUrlResponse = await sender.Send(newRequest);

                if(validateUrlResponse.Messages.Any())
                {
                    return Results.BadRequest(validateUrlResponse.Messages);
                }

                var uri = $"{host}/{validateUrlResponse.ShortUrl}";

                var result = validateUrlResponse with { ShortUrl = uri };

                return Results.Ok(result);
            });
        }

        private static string GetApiUrl(HttpContext context)
        {
            string apiUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
            return apiUrl;
        }
    }
}