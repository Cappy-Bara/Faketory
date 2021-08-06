using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Slots.Commands.CreateSlotForUser
{
    public class CreateSlotForUserCommand : IRequest<Unit>
    {
        public string UserEmail { get; set; }
    }
}
