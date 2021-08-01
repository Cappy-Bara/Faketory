using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;

namespace Faketory.Domain.Resources.PLCRelated
{
    public class IO
    {
        public Guid Id { get; set; }
        public IOType Type { get; set; }
        public int Bit { get; set; }
        public int Byte { get; set; }
        public bool Value { get; set; }
        public Guid? SlotId { get; set; }
    }
}
