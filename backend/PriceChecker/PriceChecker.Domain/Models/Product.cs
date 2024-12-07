namespace PriceChecker.Domain.Models;

public class Product
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Url { get; set; }
    public string SiteName { get; set; }
}