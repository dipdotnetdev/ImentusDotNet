
namespace CoreWebAPIPract.BackgroundServices
{
    public class DbInsertHostedService : IHostedService
    {
        private readonly ILogRepository _repository;
        private Timer _timer;
        private int _counter = 1;

        public DbInsertHostedService(ILogRepository logRepository)
        {
            _repository = logRepository;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ =>
            {
                var log = new LogEntry
                {
                    Message = $"Hosted insert #{_counter++}",
                    CreatedAt = DateTime.UtcNow
                };

                await _repository.InsetAsync(log, cancellationToken);
            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(3)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Dispose();
            return Task.CompletedTask;
        }
    }
}
