using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Application.Resources.Timestamps.Responses;
using Faketory.Domain.Aggregates;

namespace Faketory.Application.Services.Interfaces
{
    public interface ITimestampService
    {
        public Task<ModifiedUtils> Timestamp(string userEmail);

    }
}
