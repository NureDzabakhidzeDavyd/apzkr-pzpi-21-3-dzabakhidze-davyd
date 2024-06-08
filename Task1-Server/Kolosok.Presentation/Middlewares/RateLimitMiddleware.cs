using Microsoft.Extensions.Caching.Memory;

namespace Kolosok.Presentation.Middlewares;

/// <summary>
/// Represents a rate limit middleware for api.
/// Example of usage: 1..5 | ForEach-Object { Invoke-WebRequest -Uri "http://localhost:5243/api/v1/action" }
/// </summary>
public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="RateLimitMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="configuration">The configuration containing rate limit settings.</param>
    /// <param name="cache">The memory cache used for rate limiting.</param>
    public RateLimitMiddleware(RequestDelegate next, IConfiguration configuration, IMemoryCache cache)
    {
        _next = next;
        _configuration = configuration;
        _cache = cache;
    }

    /// <summary>
    /// Invokes the rate limit middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        var rateLimit = _configuration.GetValue<int>("RateLimit:Limit");
        var rateLimitPeriod = _configuration.GetValue<int>("RateLimit:Period");
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        var cacheKey = $"{ipAddress}:{DateTime.UtcNow:yyyyMMddHHmm}";

        var counter = _cache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(rateLimitPeriod);
            return 0;
        });

        if (counter >= rateLimit)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync("Too Many Requests");
            return;
        }

        counter++;
        _cache.Set(cacheKey, counter);

        await _next(context);
    }
}