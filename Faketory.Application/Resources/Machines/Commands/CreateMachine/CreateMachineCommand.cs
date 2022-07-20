using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Commands.CreateMachine
{
    public class CreateMachineCommand : IRequest<Guid>
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string UserEmail { get; set; }
        public int ProcessingTimestampAmount { get; set; }
        public int RandomFactor { get; set; }
    }
}
