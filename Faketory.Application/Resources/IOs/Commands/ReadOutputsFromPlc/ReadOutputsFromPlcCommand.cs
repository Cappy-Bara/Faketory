using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.ReadOutputsFromPlc
{
    public class ReadOutputsFromPlcCommand : IRequest<Unit>
    {
        public string[] SlotIds { get; set; }
    }
}
