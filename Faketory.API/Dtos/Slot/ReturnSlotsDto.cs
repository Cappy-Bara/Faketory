using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Slot
{
    public class ReturnSlotsDto
    {
        public IEnumerable<ReturnSlotDto> Slots { get; set; }
    }
}
