using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Domain.Enums;

namespace Faketory.Application.Resources.IOs.Commands.CreateIO
{
    public class CreateIOCommand : IRequest<Unit>
    {
        public IOType Type { get; set; }
        public int Bit { get; set; }
        public int Byte { get; set; }
        public Guid SlotId { get; set; }



        public IO GetIO()
        {
            return new IO()
            {
                SlotId = SlotId,
                Byte = Byte,
                Bit = Bit,
                Type = Type
            };
        }
    }
}
