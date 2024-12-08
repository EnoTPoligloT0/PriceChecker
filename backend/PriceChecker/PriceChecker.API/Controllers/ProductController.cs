using Microsoft.AspNetCore.Mvc;
using PriceChecker.Scrapers;

namespace PriceChecker.API.Controllers;
//todo fix to minimalApi
[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly CoreScraperService _scraperService;

    public ProductsController(CoreScraperService scraperService)
    {
        _scraperService = scraperService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchProducts(string query)
    {
        var results = await _scraperService.ScrapeAllAsync(query);
        return Ok(results);
    }
}
