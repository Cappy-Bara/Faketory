using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.DeleteSlotById
{
    public class DeleteSlotByIdCommand :IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
