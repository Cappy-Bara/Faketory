using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Commands.DeleteMachine
{
    public class DeleteMachineCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
