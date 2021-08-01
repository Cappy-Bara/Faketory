using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Resources.PLCRelated
{
    public class PlcEntity
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public string Ip { get; set; }
        public Guid? SlotId { get; set; }
        public int ModelId { get; set; }
        public virtual PlcModel Model { get; set; }

    }
}
