using Faketory.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Application.Services.Interfaces
{
    public interface ITimestampOrchestrator
    {
        public Task<ModifiedUtils> Timestamp();
    }
}
