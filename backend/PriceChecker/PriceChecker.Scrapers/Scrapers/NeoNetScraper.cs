using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;

public class NeoNetScraper(HttpClient httpClient) : BaseScraper(httpClient), ISiteScraper
{
    public string SiteName => "Neo Net";

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.neonet.pl/search.html?order=score&query={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        var searchUrl = GetSearchUrl(searchTerm);
        Console.WriteLine($"Fetching URL: {searchUrl}");  

        var html = await FetchHtmlAsync(searchUrl, "https://www.neonet.pl/search.html");

        if (string.IsNullOrEmpty(html))
        {
            Console.WriteLine("Failed to fetch HTML. Returning empty product list.");
            return new List<Product>();
        }

        Console.WriteLine("HTML fetched successfully, parsing...");

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        await File.WriteAllTextAsync("NeoNet.html", html );
        htmlDoc.LoadHtml(html);
        
        var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'listingItemScss-root')]");
    
        if (productNodes == null || !productNodes.Any())
        {
            Console.WriteLine($"No product nodes found in {SiteName}.  Debugging HTML...");
           
            return new List<Product>();
        }
        
        Console.WriteLine($"Found {productNodes.Count} product nodes. in NeoNet");
    
        return ParseProductNodes(
            productNodes,
            SiteName,
            "https://www.neonet.pl/",
            ".//h2[contains(@class, 'listingItemHeaderScss-name-')]/text()",
            ".//span[contains(@class, 'uiPriceSimpleScss-priceWrapper-')]/span[@class='uiPriceSimpleScss-priceWrapper-2zA priceByProductScss-price-1E9']/text()",
            "",
            ".//a[contains(@href, '.html')]/@href",
            false
        );
    }}