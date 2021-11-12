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
        public string UserEmail { get; set; }
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

        public void Sense(Board board)
        {
           IsSensing = NegativeLogic ? 
                !board.Pallets.Any(x => x.PosX == PosX && x.PosY == PosY) 
                :
                board.Pallets.Any(x => x.PosX == PosX && x.PosY == PosY);
        }
        public void RefreshIOState()
        {
            IO.Value = IsSensing;
        }

    }
}
