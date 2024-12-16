using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;

public class KomputronikScraper(HttpClient httpClient) : BaseScraper(httpClient), ISiteScraper
{
    public string SiteName => "Komputronik";

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.komputronik.pl/search/category/1?query={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        try
        {
            var searchUrl = GetSearchUrl(searchTerm);
            var html = await FetchHtmlAsync(searchUrl, "https://www.komputronik.pl");
            await File.WriteAllTextAsync("debugKomputronik.html", html );
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
        
            var productNodes = htmlDoc.DocumentNode.SelectNodes(
                "//div[@data-name='listingTile']");

            Console.WriteLine($"Found {productNodes.Count} products in Komputronik");
            if (productNodes == null || !productNodes.Any())
            {
                Console.WriteLine("No product nodes found on the Komputronik.");
                return new List<Product>(); 
            }

            return ParseProductNodes(
                productNodes,
                SiteName,
                "https://www.komputronik.pl",
                ".//h2[@class='line-clamp-3 font-headline text-lg font-bold leading-6 md:text-xl md:leading-8']",
                ".//div[@data-price-type='final']",
                ".//div[@class='md:col-span-2']//a[@href]/@href",
                false
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error scraping {SiteName}: {ex.Message}");
            return new List<Product>(); 
        }
    }
}