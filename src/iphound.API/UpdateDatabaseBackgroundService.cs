using iphound.API.Providers.Service.JobService;

namespace iphound.API;

public class UpdateDatabaseBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UpdateDatabaseBackgroundService> _logger;

    public UpdateDatabaseBackgroundService(IServiceProvider serviceProvider, ILogger<UpdateDatabaseBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Update Database Background Service starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var jobService = scope.ServiceProvider.GetRequiredService<UpdateDatabase>();
                    await jobService.ExecuteAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while executing the database update job.");
            }

            
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }

        _logger.LogInformation("Update database Background Service is done.");
    }
}