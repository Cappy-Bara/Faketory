﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Domain.Resources.IndustrialParts
{
    public class Pallet
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string UserEmail { get; set; }
        public bool MovementFinished { get; set; } = false;

        public Pallet()
        {
        }

        public Pallet(int x, int y)
        {
            PosX = x;
            PosY = y;
        }
    }
}