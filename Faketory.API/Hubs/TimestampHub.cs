using AutoMapper;
using Faketory.API.Dtos.ActionResponses;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.Application.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Faketory.API.Hubs
{
    public class TimestampHub : Hub
    {
        private readonly TimestampTicker _timestampTicker;

        public TimestampHub(TimestampTicker timestampTicker)
        {
            _timestampTicker = timestampTicker;
        }

        public void StartTimestamping(string userEmail)
        {
            _timestampTicker.Start(userEmail);
        }

        public async Task StopTimestamping()
        {
            await _timestampTicker.Stop();
        }
    }
}
