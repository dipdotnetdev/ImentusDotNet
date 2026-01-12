namespace CoreWebAPIPract.BackgroundServices
{
    public interface ILogRepository
    {
        Task InsetAsync(LogEntry logEntry, CancellationToken token);
    }
}
