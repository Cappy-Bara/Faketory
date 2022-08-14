using Faketory.Domain.Resources.IndustrialParts;
using MediatR;
using System.Collections.Generic;

namespace Faketory.Application.Resources.Machines.Queries.GetMachines
{
    public class GetMachinesQuery : IRequest<IEnumerable<Machine>>
    {
    }
}
