using PriceChecker.Domain.Interfaces;
using PriceChecker.Scrapers;
using PriceChecker.Scrapers.Scrapers;

namespace PriceChecker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScrapers(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<ISiteScraper, MediaMarktScraper>();

        services.AddScoped<CoreScraperService>();

        return services;
    }
}