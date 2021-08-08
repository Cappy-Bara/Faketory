using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.IOs;
using Faketory.Application.Resources.IOs.Commands.CreateIO;

namespace Faketory.API.MappingProfiles
{
    public class IOMappingProfiles : Profile
    {
        public IOMappingProfiles()
        {
            CreateMap<CreateIODto,CreateIOCommand>();
        }
    }
}
