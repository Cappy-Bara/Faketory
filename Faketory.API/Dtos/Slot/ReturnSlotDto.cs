using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Slot
{
    public class ReturnSlotDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid? PlcId { get; set; }
    }
}
