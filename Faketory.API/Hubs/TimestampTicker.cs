using Faketory.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.API.Hubs
{
    public class TimestampTicker
    {
        private string Email;

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

        public void Start(string email)
        {
            Email = email;
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
                var _timestampService = scope.ServiceProvider.GetService<ITimestampService>();
                var output = await _timestampService.Timestamp(Email);
                await _hub.Clients.All.SendAsync("timestamp", output);
            }
        }
    }
}
