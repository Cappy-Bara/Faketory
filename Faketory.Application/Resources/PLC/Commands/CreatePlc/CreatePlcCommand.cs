using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;

namespace Faketory.Application.Resources.PLC.Commands.CreatePlc
{
    public class CreatePlcCommand : IRequest<PlcEntity>
    {
        public string Ip { get; set; }
        public int ModelId { get; set; }
    }
}
