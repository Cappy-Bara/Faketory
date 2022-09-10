using Faketory.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.API.Hubs
{
    public class TimestampTicker
    {
        private readonly IHubContext<TimestampHub> _hub;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public TimestampTicker()
        {
        }

        public TimestampTicker(IHubContext<TimestampHub> hub, IServiceScopeFactory scopeFactory)
        {
            _hub = hub;
            _scopeFactory = scopeFactory;
        }

        public void Start()
        {
            _timer = new Timer(Timestamp,null,0,100);
        }

        public async Task Stop()
        {
            await _timer.DisposeAsync();
        }

        private protected async virtual void Timestamp(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var timestampOrchestrator = scope.ServiceProvider.GetService<ITimestampOrchestrator>();
                var output = await timestampOrchestrator.Timestamp();
                await _hub.Clients.All.SendAsync("timestamp", output);
            }
        }
    }
}
