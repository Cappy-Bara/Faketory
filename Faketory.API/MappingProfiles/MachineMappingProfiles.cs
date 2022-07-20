using AutoMapper;
using Faketory.API.Dtos.Machine.Responses;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.API.MappingProfiles
{
    public class MachineMappingProfiles : Profile
    {
        public MachineMappingProfiles()
        {
            CreateMap<Machine, MachineDto>();
        }
    }
}
