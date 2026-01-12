
namespace CoreWebAPIPract.BackgroundServices
{
    public class LogRepository : ILogRepository
    {
        public async Task InsetAsync(LogEntry logEntry, CancellationToken token)
        {
            await Task.Delay(1000, token);

            Console.WriteLine($"[DB INSERT] Message='{logEntry.Message}', Time={logEntry.CreatedAt}");
        }
    }
}
