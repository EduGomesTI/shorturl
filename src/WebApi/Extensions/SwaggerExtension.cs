using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ShortUrl.Api",
                    Version = "v1",
                });
            });

            return services;
        }

        public static WebApplication UseSwaggerUi(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionProvider = app.Services.GetService<IApiVersionDescriptionProvider>();

                if(apiVersionProvider is null)
                    throw new ArgumentException("API Versioning not registered.");

                foreach(var description in apiVersionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName);
                }

                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}