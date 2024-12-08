using PriceChecker.Domain.Models;
using HtmlAgilityPack;

namespace PriceChecker.Scrapers;

public abstract class BaseScraper
{
    private readonly HttpClient _httpClient;

    public BaseScraper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    
    protected async Task<string> FetchHtmlAsync(string searchUrl)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36");
        httpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.mediamarkt.pl");
        httpClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("pl");

        var response = await httpClient.GetAsync(searchUrl);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    protected List<Product> ParseProductNodes(HtmlDocument htmlDoc, string siteName, string urlPrefix, string nameXpath,
        string priceXpath, string linkXpath)
    {
        var products = new List<Product>();
        var productNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'sc-b0a2f165-0 hJZrCI sc-597dbd60-3 bWVAEq sc-3edc7bb3-2 fdepEN')]");
        
        if (productNodes == null || !productNodes.Any())
        {
            Console.WriteLine("No product nodes found.");
        }
        else
        {
            Console.WriteLine($"Found {productNodes.Count} product nodes.");
        }
        
        foreach (var node in productNodes)
        {
            var name = node.SelectSingleNode(nameXpath)?.InnerText.Trim();
            var priceText = node.SelectSingleNode(priceXpath)?.InnerText.Trim();
            var price = decimal.TryParse(new string(priceText.Where(char.IsDigit).ToArray()), out var parsedPrice) 
                ? parsedPrice / 100 : 0; 

            var linkNode = node.SelectSingleNode(linkXpath);
            var url = linkNode?.GetAttributeValue("href", "");
            
            if (!string.IsNullOrEmpty(name) && price > 0)
            {
                products.Add(new Product
                {
                    ProductName = name,
                    Price = price,
                    Url = $"{urlPrefix}{url}",
                    SiteName = siteName
                });
                Console.WriteLine($"Product: {name}, Price: {price}, URL: {url}");
            }
        }

        return products;
    }

    public abstract string GetSearchUrl(string searchTerm);
}