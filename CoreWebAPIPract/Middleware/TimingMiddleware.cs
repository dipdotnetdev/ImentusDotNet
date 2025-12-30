namespace CoreWebAPIPract.Middleware
{
    public class TimingMiddleware
    {
        public readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public TimingMiddleware(RequestDelegate next, ILogger logger) 
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invole(HttpContext context)
        {
            var start = DateTime.Now;
            await _next(context);
            var duration = DateTime.Now - start;
            _logger.LogInformation($"Request took {duration.TotalMilliseconds} ms");
        }
    }
}
