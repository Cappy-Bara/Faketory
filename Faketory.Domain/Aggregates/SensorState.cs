using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class SensorState
    {
        public Guid Id { get; set; }
        public bool IsSensing { get; set; }

        public SensorState(Sensor sensor)
        {
            Id = sensor.Id;
            IsSensing = sensor.IsSensing;
        }
    }
}
