using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Queries.GetConveyor
{
    public class GetConveyorQuery : IRequest<Conveyor>
    {
        public Guid ConveyorId { get; set; }
    }
}
