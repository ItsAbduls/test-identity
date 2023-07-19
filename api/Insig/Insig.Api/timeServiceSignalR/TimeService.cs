using Insig.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Insig.Api.timeServiceSignalR
{
    public class TimeService : IHostedService, IDisposable
    {
        private readonly IHubContext<TimeHub> timeHubContext;
        Timer timer;
        public TimeService(IHubContext<TimeHub> timeHubContext)
        {
            this.timeHubContext = timeHubContext;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Timer(callback, values to pass, when to start, how often start again and again);
            timer = new Timer(Tick, null, 0, 500);
            return Task.CompletedTask;
        }

        private void Tick(object state)
        {
            var currentTime = DateTime.UtcNow.ToString("F");
            timeHubContext.Clients.All.SendAsync("updateCurrentTime", currentTime);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
