using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.Resources.IndustrialParts
{
    public class Sensor
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public Guid IOId { get; set; }
        public virtual IO IO { get; set; }
        public bool IsSensing { get; set; }
        public bool NegativeLogic { get; set; }


        public Sensor()
        {
        }
        public Sensor(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        public Pallet Sense(IEnumerable<Pallet> pallets)
        {
            pallets ??= new List<Pallet>();
            var pallet = pallets.FirstOrDefault(x => x.PosX == PosX && x.PosY == PosY);

            IsSensing = NegativeLogic ? (pallet is null) : !(pallet is null);

            return pallet;
        }
        public bool RefreshIOState()
        {
            if (IO is null)
                return false;
            var stateChanged = IO.Value != IsSensing;
            IO.Value = IsSensing;
            return stateChanged;
        }
    }
}
