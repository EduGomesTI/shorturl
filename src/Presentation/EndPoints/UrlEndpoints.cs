using Application.Urls.Commands.CreateUrl;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Presentation.EndPoints
{
    public class UrlEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/url", async (CreateUrlCommand request, ISender sender) =>
            {
                var result = await sender.Send(request);

                return Results.Ok(result);
            });
        }
    }
}