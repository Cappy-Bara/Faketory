using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.IOs;
using Faketory.API.Dtos.IOs.Requests;
using Faketory.Application.Resources.IOs.Commands.CreateIO;
using Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots;

namespace Faketory.API.MappingProfiles
{
    public class IOMappingProfiles : Profile
    {
        public IOMappingProfiles()
        {
            CreateMap<CreateIODto,CreateIOCommand>();
            CreateMap<UpdateIOStatusesDto, WriteInputsToPlcQuery>()
                .ForMember(x => x.SlotIds, z => z.MapFrom(k=> k.Ids));
        }
    }
}
