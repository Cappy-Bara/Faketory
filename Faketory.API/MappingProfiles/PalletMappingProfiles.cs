using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Faketory.API.Dtos.Pallets.Responses;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.API.MappingProfiles
{
    public class PalletMappingProfiles : Profile
    {
        public PalletMappingProfiles()
        {
            CreateMap<Pallet, PalletDto>();
        }
    }
}
