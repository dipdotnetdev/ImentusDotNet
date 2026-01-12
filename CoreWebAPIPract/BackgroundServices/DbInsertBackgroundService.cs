
namespace CoreWebAPIPract.BackgroundServices
{
    public class DbInsertBackgroundService: BackgroundService
    {
        private readonly ILogRepository _logRepository;

        public DbInsertBackgroundService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("DB background service started");

            int Counter = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                var log = new LogEntry
                {
                    Message = $"Background insert: {Counter++}",
                    CreatedAt = DateTime.UtcNow,
                };

                await _logRepository.InsetAsync(log, stoppingToken);

                await Task.Delay(1000, stoppingToken);

                Console.WriteLine("DB Background Service stopping...");
            }
        }
    }
}
