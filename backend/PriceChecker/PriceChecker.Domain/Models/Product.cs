namespace PriceChecker.Domain.Models;

public class Product
{
    private Product(string productName, decimal price,  string imageUrl, string url, string siteName)
    {
        ProductName = productName;
        Price = price;
        ImageUrl = imageUrl;
        Url = url;
        SiteName = siteName;
    }

    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl{get; set;}
    public string Url { get; set; }
    public string SiteName { get; set; }
    
    public static Product Create(string productName, decimal price, string imageUrl, string url, string siteName)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name cannot be null or empty.", nameof(productName));
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        if (string.IsNullOrWhiteSpace(siteName))
            throw new ArgumentException("Site name cannot be null or empty.", nameof(siteName));

        return new Product(productName, price, imageUrl, url, siteName );
    }
}