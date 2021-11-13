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
        public Pallet Pallet { get; }
        public Guid Id { get => Pallet.Id; set => Pallet.Id = value; }
        public int PrevPosX { get; set; }
        public int PrevPosY { get; set; }
        public int PosX { get => Pallet.PosX; set => Pallet.PosX = value; }
        public int PosY { get => Pallet.PosY; set => Pallet.PosY = value; }
        public MovePriority MovePriority { get; set; } = MovePriority.Still;
        public (int,int) NewPosition{ get => (PosX,PosY);}
       
        public MovedPallet(Pallet pallet)
        {
            Pallet = pallet;
            Id = pallet.Id;
            PrevPosX = pallet.PosX;
            PrevPosY = pallet.PosY;
        }
        
        public void UndoMovement()
        {
            PosX = PrevPosX;
            PosY = PrevPosY;
        }
    }
}
