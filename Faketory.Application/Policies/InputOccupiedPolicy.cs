using System;
using System.Threading.Tasks;
using Faketory.Domain.IPolicies;
using Faketory.Domain.IRepositories;

namespace Faketory.Application.Policies
{
    public class InputOccupiedPolicy : IInputOccupiedPolicy
    {
        private readonly ISensorRepository _sensorRepo;

        public InputOccupiedPolicy(ISensorRepository sensorRepo)
        {
            _sensorRepo = sensorRepo;
        }

        public async Task<bool> IsOccupied(Guid inputId, Guid? sensorId = null)
        {
            Guid NotNullSensorId = sensorId ?? Guid.Empty;

            return await _sensorRepo.IOOccupiedBySensor(inputId, NotNullSensorId);
        }
    }
}
