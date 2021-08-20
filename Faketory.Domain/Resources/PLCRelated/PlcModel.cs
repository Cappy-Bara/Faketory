using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace Faketory.Domain.Resources.PLCRelated
{
    public class PlcModel
    {
        public int CpuModel { get; set; }
        public CpuType Cpu { get; set; }
        public short Rack { get; set; }
        public short Slot { get; set; }
    }
}
