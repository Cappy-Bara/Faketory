using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Queries.GetSensors
{
    public class GetSensorsHandler :IRequestHandler<GetSensorsQuery,List<Sensor>>
    {
        private readonly ISensorRepository _sensorRepo;

        public GetSensorsHandler(ISensorRepository sensorRepo)
        {
            _sensorRepo = sensorRepo;
        }

        public async Task<List<Sensor>> Handle(GetSensorsQuery request, CancellationToken cancellationToken)
        {
            return await _sensorRepo.GetUserSensors(request.UserEmail);
        }
    }
}
