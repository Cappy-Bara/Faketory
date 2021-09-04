using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.Conveyors.Responses;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.API.MappingProfiles
{
    public class ConveyorMappingProfiles
    {
        public class PlcMappingProfiles : Profile
        {
            public PlcMappingProfiles()
            {
                CreateMap<Conveyor, ConveyorDto>()
                    .ForMember(x => x.Bit, y => y.MapFrom(z => z.IO.Bit))
                    .ForMember(x => x.Byte, y => y.MapFrom(z => z.IO.Byte))
                    .ForMember(x => x.Slot, y => y.MapFrom(z => z.IO.SlotId));
            }
        }
    }
}
