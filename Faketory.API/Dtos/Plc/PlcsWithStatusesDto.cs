using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos
{
    public class PlcsWithStatusesDto
    {
        public IEnumerable<PlcWithStatusDto> Plcs {get;set;}
    }
}
