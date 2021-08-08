using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.BindPlcToSlot
{
    public class BindPlcToSlotCommand : IRequest<Unit>
    {
        public Guid PlcId { get; set; }
        public Guid SlotId { get; set; }
    }
}
