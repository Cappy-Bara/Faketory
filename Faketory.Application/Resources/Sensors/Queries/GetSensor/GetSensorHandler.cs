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
        private readonly IUserRepository _userRepo;

        public GetSensorHandler(IUserRepository userRepo, ISensorRepository sensorRepo)
        {
            _userRepo = userRepo;
            _sensorRepo = sensorRepo;
        }



        public async Task<Sensor> Handle(GetSensorQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
            {
                throw new NotFoundException("User does not exist!");
            }

            var sensor = await _sensorRepo.GetSensorById(request.SensorId);
            if (sensor == null)
            {
                throw new NotFoundException("This sensor does not exist.");
            }

            if (sensor.UserEmail != request.UserEmail)
            {
                throw new BadRequestException("This sensor is not yours!");
            }

            return sensor;
        }
    }
}
