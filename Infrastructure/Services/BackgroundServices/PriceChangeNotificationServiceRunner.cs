using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.BackgroundServices
{
    public class PriceChangeNotificationServiceRunner : IHostedService, IDisposable
    {
        private readonly ILogger<PriceChangeNotificationServiceRunner> _logger;
        private readonly IPriceChangeNotificationService _notificationService;
        private readonly IUnitOfWork _uow;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public PriceChangeNotificationServiceRunner(ILogger<PriceChangeNotificationServiceRunner> logger,
            IPriceChangeNotificationService notificationService, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _notificationService = notificationService;
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Runner started");
            _timer = new Timer(x => Job(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Runner stopped");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void Job()
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                _notificationService.NotifyAll(scope.ServiceProvider.GetRequiredService<IUnitOfWork>());
                _logger.LogInformation($"It's {DateTime.Now}");
            }

        }
    }
}
