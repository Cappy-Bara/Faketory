using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Enums;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class MovedPallet
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public MovePriority MovePriority { get; set; } = MovePriority.Still;

        public MovedPallet(Pallet pallet)
        {
            Id = pallet.Id;
            PosX = pallet.PosX;
            PosY = pallet.PosY;
        }
    }
}
