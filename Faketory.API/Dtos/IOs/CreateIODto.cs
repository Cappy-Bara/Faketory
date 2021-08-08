using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;

namespace Faketory.API.Dtos.IOs
{
    public class CreateIODto
    {
        public IOType Type { get; set; }
        public int Bit { get; set; }
        public int Byte { get; set; }
        public Guid SlotId { get; set; }
    }
}
