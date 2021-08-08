using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos
{
    public class GetPlcsResponse
    {
        public IEnumerable<GetPlcResponse> Plcs { get; set; }
    }
}
