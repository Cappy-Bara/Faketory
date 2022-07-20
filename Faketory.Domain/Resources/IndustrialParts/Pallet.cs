using System;
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
        public bool IsProcessed { get; private set; } = false;

        private Pallet()
        {
        }
        public Pallet(int x, int y)
        {
            PosX = x;
            PosY = y;
        }

        public void MoveTop()
        {
            if (!IsProcessed)
                PosY++;
        }
        public void MoveBottom()
        {
            if (!IsProcessed)
                PosY--;
        }
        public void MoveRight()
        {
            if (!IsProcessed)
                PosX++;
        }
        public void MoveLeft()
        {
            if (!IsProcessed)
                PosX--;
        }

        public void Process()
        {
            IsProcessed = true;
        }
    }
}
