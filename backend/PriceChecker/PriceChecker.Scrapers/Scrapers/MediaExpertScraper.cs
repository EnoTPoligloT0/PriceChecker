using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;

public class MediaExpertScraper : BaseScraper, ISiteScraper
{
    public string SiteName => "Media Expert";

    public MediaExpertScraper(HttpClient httpClient) : base(httpClient) { }

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.mediaexpert.pl/search?query[menu_item]=&query[querystring]={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        var searchUrl = GetSearchUrl(searchTerm);
        Console.WriteLine($"Fetching URL: {searchUrl}");  

        var html = await FetchHtmlAsync(searchUrl, "https://www.mediaexpert.pl");

        if (string.IsNullOrEmpty(html))
        {
            Console.WriteLine("Failed to fetch HTML. Returning empty product list.");
            return new List<Product>();
        }

        Console.WriteLine("HTML fetched successfully, parsing...");

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);

        var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'offer-box')]");
    
        if (productNodes == null || !productNodes.Any())
        {
            Console.WriteLine("No product nodes found. Debugging HTML...");
           
            return new List<Product>();
        }
        
        Console.WriteLine($"Found {productNodes.Count} product nodes.");
    
        return ParseProductNodes(
            productNodes,
            SiteName,
            "https://www.mediaexpert.pl",
            ".//a[contains(@class, 'is-animate ui-link')]",
            ".//span[contains(@class, 'whole')]",
            ".//a[contains(@class, 'is-animate ui-link')]/@href",
            false
        );
    }}