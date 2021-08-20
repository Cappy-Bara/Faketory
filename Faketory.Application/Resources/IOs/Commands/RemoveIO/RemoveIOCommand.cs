using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.RemoveIO
{
    public class RemoveIOCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
