using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Application.Resources.Timestamps.Responses;

namespace Faketory.Application.Services.Interfaces
{
    public interface ITimestampService
    {
        public Task<DynamicUtils> Timestamp(string userEmail);

    }
}
