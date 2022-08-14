using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Queries.GetSensor
{
    public class GetSensorHandler : IRequestHandler<GetSensorQuery, Sensor>
    {
        private readonly ISensorRepository _sensorRepo;

        public GetSensorHandler(ISensorRepository sensorRepo)
        {
            _sensorRepo = sensorRepo;
        }

        public async Task<Sensor> Handle(GetSensorQuery request, CancellationToken cancellationToken)
        {
            var sensor = await _sensorRepo.GetSensorById(request.SensorId);
            if (sensor == null)
            {
                throw new NotFoundException("This sensor does not exist.");
            }

            return sensor;
        }
    }
}
