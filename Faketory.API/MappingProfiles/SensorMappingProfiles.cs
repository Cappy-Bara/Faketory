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
            CreateMap<Sensor,SensorDto>()
                .ForMember(x => x.Bit, y => y.MapFrom(z => z.IO.Bit))
                .ForMember(x => x.Byte, y => y.MapFrom(z => z.IO.Byte))
                .ForMember(x => x.SlotId, y => y.MapFrom(z => z.IO.SlotId));
        }
    }
}
