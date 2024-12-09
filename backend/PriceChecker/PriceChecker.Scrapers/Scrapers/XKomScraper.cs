using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;


public class XKomScraper(HttpClient httpClient) : BaseScraper(httpClient), ISiteScraper
{
    public string SiteName => "x-kom";

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.x-kom.pl/szukaj?q={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        var searchUrl = GetSearchUrl(searchTerm);
        var html = await FetchHtmlAsync(searchUrl, "https://www.x-kom.pl");
        
        Console.WriteLine(searchUrl);
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var productNodes = htmlDoc.DocumentNode.SelectNodes(
            "//div[contains(@class, 'parts__LinkWithoutAnchorWrapper-sc-f5aee401-0')]");
    
        if (productNodes == null || !productNodes.Any())
        {
            
            Console.WriteLine("No product nodes found on the x-kom.");
            return new List<Product>();
        }

        return ParseProductNodes(
            productNodes,
            SiteName,
            "https://www.x-kom.pl",
            ".//h3[contains(@class, 'parts__Title-sc-1d28d-0')]",
            ".//span[contains(@class, 'parts__Price-sc-6e255ce0-0')]",
            ".//a[contains(@class, 'parts__StyledLink-sc-4e18e67b-0')]/@href"
        );
    }
}