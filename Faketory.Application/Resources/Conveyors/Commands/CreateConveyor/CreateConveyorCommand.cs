﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Faketory.Application.Resources.Conveyors.Commands.CreateConveyor
{
    public class CreateConveyorCommand : IRequest<Guid>
    {
        public Guid SlotId { get; set; }
        public string UserEmail { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Length { get; set; }
        public bool IsVertical { get; set; }
        public bool IsTurnedDownOrLeft { get; set; }
        public int Frequency { get; set; }
        public int Bit { get; set; }
        public int Byte { get; set; }
        public bool NegativeLogic { get; set; }
    }
}
