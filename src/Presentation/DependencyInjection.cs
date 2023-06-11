using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Workers;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddCarter();

            services.AddHostedService<SendMessagesWorker>();

            return services;
        }

        public static WebApplication UsePresentation(this WebApplication app)
        {
            app.MapCarter();

            return app;
        }
    }
}