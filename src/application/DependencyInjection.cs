using Application.Urls.Commands.CallOriginalUrl;
using Application.Urls.Commands.CreateUrl;
using Application.Urls.Commands.ValidateUrl;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IValidator<CallOriginalUrlCommand>, CallOriginalUrlValidator>();
            services.AddScoped<IValidator<CreateUrlCommand>, CreateUrlValidator>();
            services.AddScoped<IValidator<ValidateUrlCommand>, ValidateUrlValidator>();

            return services;
        }
    }
}