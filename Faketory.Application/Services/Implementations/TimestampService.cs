using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Application.Resources.Timestamps.Responses;
using Faketory.Application.Services.Interfaces;

namespace Faketory.Application.Services.Implementations
{
    public class TimestampService : ITimestampService
    {

        public Task<DynamicUtils> Timestamp(string userEmail)
        {
            throw new NotImplementedException();
        }
    }
}
