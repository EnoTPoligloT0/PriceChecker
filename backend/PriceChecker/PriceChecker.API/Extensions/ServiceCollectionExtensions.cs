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
        services.AddScoped<ISiteScraper, MediaExpertScraper>();
        services.AddScoped<ISiteScraper, XKomScraper>();
        services.AddScoped<ISiteScraper, KomputronikScraper>();
        // services.AddScoped<ISiteScraper, NeoNetScraper>();
        // services.AddScoped<ISiteScraper, RtvEuroAgdScraper>();

        services.AddScoped<CoreScraperService>();

        return services;
    }
}