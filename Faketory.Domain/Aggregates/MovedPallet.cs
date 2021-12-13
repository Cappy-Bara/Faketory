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
        private MovePriority _lastMovePriority;
        private MovePriority _movePriority;
        public Guid Id { get => Pallet.Id; set => Pallet.Id = value; }
        public int PrevPosX { get; set; }
        public int PrevPosY { get; set; }
        public int PosX { get => Pallet.PosX; set => Pallet.PosX = value; }
        public int PosY { get => Pallet.PosY; set => Pallet.PosY = value; }
        public MovePriority MovePriority { 
            get 
            {
                return _movePriority;
            }
            set 
            {
                _lastMovePriority = _movePriority;
                _movePriority = value;
            } 
        }
        public (int,int) NewPosition{ get => (PosX,PosY);}
        public bool AlreadyMoved { get => PrevPosX == PosX && PrevPosY == PosY;}

        public MovedPallet(Pallet pallet)
        {
            Pallet = pallet;
            Id = pallet.Id;
            PrevPosX = pallet.PosX;
            PrevPosY = pallet.PosY;
            _movePriority = MovePriority.Still;
            _lastMovePriority = MovePriority.Still;
        }
        
        public void UndoMovement()
        {
            PosX = PrevPosX;
            PosY = PrevPosY;
            _movePriority = _lastMovePriority;
        }
    }
}
