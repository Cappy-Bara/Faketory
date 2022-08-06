using Faketory.Domain.Resources.IndustrialParts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Queries.GetMachine
{
    public class GetMachineQuery : IRequest<Machine>
    {
        public Guid MachineId { get; set; }
        public string UserEmail { get; set; }
    }
}
