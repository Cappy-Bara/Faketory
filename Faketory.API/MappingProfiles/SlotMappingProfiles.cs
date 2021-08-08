using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.Slot;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.API.MappingProfiles
{
    public class PlcSlotProfiles : Profile
    {
        public PlcSlotProfiles()
        {
            CreateMap<Slot, ReturnSlotDto>();
        }
    }
}
