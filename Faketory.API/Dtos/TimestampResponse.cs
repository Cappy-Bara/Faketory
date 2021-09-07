using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Pallets.Responses;

namespace Faketory.API.Dtos
{
    public class TimestampResponse
    {
        public List<PalletDto> Pallets { get; set; }
    }
}
