﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.IOs.Commands.RefreshIOStatusInChosenSlots
{
    public class WriteInputsToPlcQuery : IRequest<Unit>
    {
        public string[] SlotIds { get; set; }
    }
}
