using PriceChecker.Domain.Models;

namespace PriceChecker.Domain.Interfaces;

public interface ISiteScraper
{
    string SiteName { get; }
    Task<List<Product>> ScrapeProductsAsync(string searchTerm);
}