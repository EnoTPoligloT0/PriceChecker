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

    
    protected async Task<string> FetchHtmlAsync(string searchUrl, string referrer = "")
    {
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, searchUrl);
        requestMessage.Headers.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
        if (!string.IsNullOrWhiteSpace(referrer))
        {
            requestMessage.Headers.Referrer = new Uri(referrer);
        }
        requestMessage.Headers.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
        await Task.Delay(500); 
        var response = await _httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    protected List<Product> ParseProductNodes(HtmlNodeCollection productNodes, string siteName, string urlPrefix, string nameXpath,
        string priceXpath, string linkXpath, bool formatPrice = true)
    {
        
        var products = new List<Product>();
       
        if (productNodes == null)
        {
            Console.WriteLine("No product nodes found.");
            return products;
        }
        if (!productNodes.Any())
        {
            Console.WriteLine("Product nodes are empty.");
            return products;
        }
        
        Console.WriteLine($"Found {productNodes.Count} product nodes.");
        Console.WriteLine($"{productNodes} in site: {siteName}");
        
        foreach (var node in productNodes)
        {
            var name = node.SelectSingleNode(nameXpath)?.InnerText.Trim();
            var priceText = node.SelectSingleNode(priceXpath)?.InnerText.Trim();
            var price = decimal.TryParse(new string(priceText.Where(char.IsDigit).ToArray()), out var parsedPrice) 
                ? formatPrice
                    ? parsedPrice / 100 : parsedPrice 
                : 0; 
            Console.WriteLine(name);
            Console.WriteLine(price);
            Console.WriteLine(siteName);
            var linkNode = node.SelectSingleNode(linkXpath);
            var url = linkNode?.GetAttributeValue("href", "");
            Console.WriteLine(url);
            if (!string.IsNullOrEmpty(name) && price > 0)
            {
                products.Add(Product.Create(name, price, $"{urlPrefix}{url}", siteName));
                Console.WriteLine(Product.Create(name, price, $"{urlPrefix}{url}", siteName));
            }
        }

        return products;
    }

    public abstract string GetSearchUrl(string searchTerm);
}