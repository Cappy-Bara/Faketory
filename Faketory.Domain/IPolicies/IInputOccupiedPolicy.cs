using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.IPolicies
{
    public interface IInputOccupiedPolicy
    {
        public Task<bool> IsOccupied(Guid inputId, Guid? sensorId);
    }
}
