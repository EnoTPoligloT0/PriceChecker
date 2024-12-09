using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;

public class MediaMarktScraper(HttpClient httpClient) : BaseScraper(httpClient), ISiteScraper
{
    public string SiteName => "Media Markt";

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.mediamarkt.pl/pl/search.html?query={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        var searchUrl = GetSearchUrl(searchTerm);
        var html = await FetchHtmlAsync(searchUrl, "https://www.mediamarkt.pl");

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        
        var productNodes = htmlDoc.DocumentNode.SelectNodes(
            "//div[contains(@class, 'sc-b0a2f165-0 hJZrCI sc-597dbd60-3 bWVAEq sc-3edc7bb3-2 fdepEN')]");

        if (productNodes == null || !productNodes.Any())
        {
            Console.WriteLine("No product nodes found on the MediaMarkt.");
            return new List<Product>(); 
        }
        return ParseProductNodes(
            productNodes,
            SiteName,
            "https://www.mediamarkt.pl",
            ".//p[contains(@class, 'sc-8b815c14-0 dbwSez')]",
            ".//span[contains(@class, 'sc-e0c7d9f7-0 bPkjPs')]",
            ".//a[contains(@class, 'sc-2fa46f1d-1 hHoKle')]/@href"
        );
    }
}