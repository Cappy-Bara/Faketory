using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.Sensors.Responses;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.API.MappingProfiles
{
    public class SensorMappingProfiles :Profile
    {
        public SensorMappingProfiles()
        {
            CreateMap<Sensor,SensorDto>();
        }
    }
}
