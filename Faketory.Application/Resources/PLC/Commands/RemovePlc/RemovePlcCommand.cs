using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.PLC.Commands.RemovePlc
{
    public class RemovePlcCommand : IRequest<Unit>
    {
        public Guid PlcId { get; set; }
    }
}
