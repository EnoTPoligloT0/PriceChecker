using HtmlAgilityPack;
using PriceChecker.Domain.Interfaces;
using PriceChecker.Domain.Models;

namespace PriceChecker.Scrapers.Scrapers;

public class RtvEuroAgdScraper(HttpClient httpClient) : BaseScraper(httpClient), ISiteScraper
{
    public string SiteName => "RTV Euro Agd";

    public override string GetSearchUrl(string searchTerm)
    {
        return $"https://www.euro.com.pl/search.bhtml?keyword={Uri.EscapeDataString(searchTerm)}";
    }

    public async Task<List<Product>> ScrapeProductsAsync(string searchTerm)
    {
        Console.WriteLine($"Scraping on RTV Euro Agd: {searchTerm}");
        var searchUrl = GetSearchUrl(searchTerm);
        var html = await FetchHtmlAsync(searchUrl, "https://www.euro.com.pl");

        
        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        
        var productNodes = htmlDoc.DocumentNode.SelectNodes(
            "//div[contains(@class, 'product-medium-box ng-star-inserted')]");
        Console.WriteLine($"Found {productNodes.Count} products on RTV euro agd");
        
        if (productNodes == null || !productNodes.Any())
        {
            Console.WriteLine("No product nodes found on the RTV.");
            File.WriteAllText("debug.html", html);
            return new List<Product>();
            
        }
        File.WriteAllText("debugRTV.html", html);
        return ParseProductNodes(
            productNodes,
            SiteName,
            "https://www.euro.com.pl",
            ".//h4[contains(@class, 'product-medium-box-intro__title')]",
            ".//span[contains(@class, 'product-medium-box-intro__rating')]",
            "",
            ".//a[contains(@class, 'product-medium-box-intro__link')]/@href"
        );
    }
}