using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos;
using Faketory.API.Dtos.Plc;
using Faketory.API.Dtos.Plc.Requests;
using Faketory.API.Dtos.Plc.Responses;
using Faketory.Application.Resources.PLC.Commands.CreatePlc;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.API.MappingProfiles
{
    public class PlcMappingProfiles : Profile
    {
        public PlcMappingProfiles()
        {
            CreateMap<CreatePlcDto, CreatePlcCommand>();
            CreateMap<PlcEntity, GetPlcResponse>();
            CreateMap<PlcConnectionStatus, PlcWithStatusDto>();

        }
    }
}
