using System.ComponentModel;

namespace JobBoardAPI.Services
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundService> _logger;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<BackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        // this is for testing background worker 
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var scope = _scopeFactory.CreateScope();
                var serv = scope.ServiceProvider.GetRequiredService<IApplicantService>();

                var guid = Guid.NewGuid().ToString().Replace("-","");
                var firstName = guid.Substring(0, 5);
                var lastName = guid.Substring(6, 5);
                var isCreated=serv.CreateAsync(new Dtos.ApplicantDto(ApplicantId: 0, Name: firstName + " " + lastName, Email: firstName + "." + lastName + "@example.com", IsActive: true));                

                _logger.LogInformation("New applicant created >" + isCreated + " - Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
