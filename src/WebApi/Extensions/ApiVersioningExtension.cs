using Microsoft.AspNetCore.Mvc;

namespace WebApi.Extensions
{
    public static class ApiVersioningExtension
    {
        public static IServiceCollection AddApiVersioningSetup(this IServiceCollection services)
        {
            services.AddApiVersioning(
                    options =>
                    {
                        options.ReportApiVersions = true;
                        options.AssumeDefaultVersionWhenUnspecified = true;
                        options.DefaultApiVersion = new ApiVersion(1, 0);
                    })
                .AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}