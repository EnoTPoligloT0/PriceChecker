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
        var html = await FetchHtmlAsync(searchUrl);

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        
        
        return ParseProductNodes(
            htmlDoc,
            SiteName,
            "https://www.mediamarkt.pl",
            ".//p[contains(@class, 'sc-8b815c14-0 dbwSez')]",
            ".//span[contains(@class, 'sc-e0c7d9f7-0 bPkjPs')]",
            ".//a[contains(@class, 'sc-2fa46f1d-1 hHoKle')]/@href"
        );
    }
}