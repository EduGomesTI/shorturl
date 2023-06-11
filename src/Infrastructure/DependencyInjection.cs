using Application.Abstractions.Data;
using Domain.Repositories;
using Infrastructure.Options;
using Infrastructure.Persistences;
using Infrastructure.Persistences.DbContexts;
using Infrastructure.Persistences.Urls;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ShortUrlDbContext>((serviceprovider, dbContextOptionsBuilder) =>
            {
                var databseOptions = serviceprovider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseNpgsql(databseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databseOptions.MaxRetryCount);

                    options.CommandTimeout(databseOptions.CommandTimeOut);

                    options.MigrationsHistoryTable(databseOptions.MigrationHistoryTable);
                });

                dbContextOptionsBuilder.LogTo(x => Console.WriteLine(x));

                dbContextOptionsBuilder.EnableDetailedErrors(databseOptions.EnabledDetailedErrors);

                dbContextOptionsBuilder.EnableSensitiveDataLogging(databseOptions.EnabledSensitiveDataLogging);
            });

            services.AddDbContext<OutboxDbContext>((serviceprovider, dbContextOptionsBuilder) =>
            {
                var databseOptions = serviceprovider.GetService<IOptions<DatabaseOptions>>()!.Value;

                dbContextOptionsBuilder.UseNpgsql(databseOptions.ConnectionString, options =>
                {
                    options.EnableRetryOnFailure(databseOptions.MaxRetryCount);

                    options.CommandTimeout(databseOptions.CommandTimeOut);

                    options.MigrationsHistoryTable(databseOptions.MigrationHistoryTable);
                });

                dbContextOptionsBuilder.LogTo(x => Console.WriteLine(x));

                dbContextOptionsBuilder.EnableDetailedErrors(databseOptions.EnabledDetailedErrors);

                dbContextOptionsBuilder.EnableSensitiveDataLogging(databseOptions.EnabledSensitiveDataLogging);
            });

            services.AddScoped<IUnitOfWork, UnitOfWOrk>();

            services.AddScoped<IUrlRepository, UrlRepository>();

            return services;
        }
    }
}