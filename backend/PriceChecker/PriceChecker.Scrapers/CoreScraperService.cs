using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;
using PriceChecker.Scrapers.Scrapers;

namespace PriceChecker.Scrapers;

public class CoreScraperService
{
    private readonly IEnumerable<ISiteScraper> _scrapers;

    public CoreScraperService(IEnumerable<ISiteScraper> scrapers)
    {
        _scrapers = scrapers;
    }

    public async Task<List<Product>> ScrapeAllAsync(string query)
    {
        var tasks = _scrapers.Select(scraper => scraper.ScrapeProductsAsync(query));
        var results = await Task.WhenAll(tasks);
        
        return results.SelectMany(r => r)
            .OrderBy(p => p.Price)
            .ToList();
    }
}