using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Commands.RemoveSensor
{
    public class RemoveSensorHandler : IRequestHandler<RemoveSensorCommand, Unit>
    {
        private readonly ISensorRepository _sensorRepo;

        public RemoveSensorHandler(ISensorRepository sensorRepo)
        {
            _sensorRepo = sensorRepo;
        }

        public async Task<Unit> Handle(RemoveSensorCommand request, CancellationToken cancellationToken)
        {
            if (!await _sensorRepo.SensorExist(request.SensorId))
                throw new NotFoundException("Sensor with this Id does not exist");

            await _sensorRepo.RemoveSensor(request.SensorId);
            return Unit.Value;
        }
    }
}
