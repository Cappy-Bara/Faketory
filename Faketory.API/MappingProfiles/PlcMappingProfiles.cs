using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos;
using Faketory.Application.Resources.PLC.Commands.CreatePlc;

namespace Faketory.API.MappingProfiles
{
    public class PlcMappingProfiles : Profile
    {
        public PlcMappingProfiles()
        {
            CreateMap<CreatePlcDto, CreatePlcCommand>();

        }
    }
}
