namespace CoreWebAPIPract.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("before middleware");
            await _next(context);
            Console.WriteLine("after middleware");
        }
    }
}
