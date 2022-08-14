using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Sensors.Queries.GetSensor
{
    public class GetSensorQuery :IRequest<Sensor>
    {
        public Guid SensorId { get; set; }
    }
}
