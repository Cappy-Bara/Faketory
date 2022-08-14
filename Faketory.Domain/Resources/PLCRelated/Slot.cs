using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Resources.PLCRelated
{
    public class Slot
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public Guid? PlcId { get; set; }
        public virtual PlcEntity Plc { get; set; }
        public virtual List<IO> InputsOutputs { get; set; }
    }
}
