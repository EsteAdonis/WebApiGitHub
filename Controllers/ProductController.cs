using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace WebApiGitHub.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IMemoryCache cache, ILogger Logger) : ControllerBase
	{
		private readonly IMemoryCache _cache = cache;
		private readonly ILogger _Logger = Logger;
		private readonly string cacheKey = "productCacheKey";

		[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Products>>> Index()
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				_Logger.Log(LogLevel.Information, "Products found in cache");
			}
			else
			{
				products = _context.Products.toList();
				var cacheEntryOptions = new MemoryCacheEntryOptions()
																		.SetSlidingExpiration(TimeSpan.FromSeconds(45))
																		.SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
																		.SetPriority(CacheItemPriority.Normal);
				_cache.Set(cacheKey, products, cacheEntryOptions);
			}
			stopwatch.Stop();
			_Logger.Log(LogLevel.Information, "Passed time " + stopwatch.ElapsedMilliseconds);
			return Ok(products);
		}

		public IActionResult ClearCache()
		{
			_cache.Remove(cacheKey);
			_Logger.Log(LogLevel.Information, "Cleared cached");
			return RedirectToAction("Index");
		}
	}
}

