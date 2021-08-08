using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.API.Dtos.Plc
{
    public class CreatePlcDto
    {
        public string UserEmail { get; set; }
        public string Ip { get; set; }
        public Guid SlotId { get; set; }
        public int ModelId { get; set; }
    }
}
