namespace Sample.DotNetAspire.FirstWorker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("[FirstWorker] Send api request");

            using var httpClient = new HttpClient() { BaseAddress = new("https://localhost:7461") };
            var response = await httpClient.GetAsync("/health", stoppingToken);
            var content = await response.Content.ReadAsStringAsync(stoppingToken);

            logger.LogInformation("[FirstWorker] Response: {Response}", content);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
