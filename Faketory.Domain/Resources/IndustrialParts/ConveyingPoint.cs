using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;

namespace Faketory.Domain.Resources.IndustrialParts
{
    public class ConveyingPoint
    {
        public Guid Id { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool LastPoint { get; set; }
        public bool Delay { get; set; } = true;
        public virtual Pallet PalletToMove { get; set; }
        public Guid ConveyorId { get; set; }
        public virtual Conveyor Conveyor { get; set; }

        public ConveyingPoint(int x, int y)
        {
            PosX = x;
            PosY = y;
        }
        public ConveyingPoint()
        {
        }
        public void MarkPalletAsMoved()
        {
            if (PalletToMove != null)
                PalletToMove.MovementFinished = true;
        }
        private void UpdatePalletPosition(int x, int y)
        {
            PalletToMove.PosX = x;
            PalletToMove.PosY = y;
        }
        public void MovePalletAtPoint(bool isVertical, bool isTurnedDownOrLeft, Scene scene)
        {
            if (PalletToMove == null || PalletToMove.MovementFinished)
                return;

            var newX = PosX;
            var newY = PosY;

            int sign = isTurnedDownOrLeft ? -1 : 1;

            if (isVertical)
            {
                newY += sign;
                if (scene.NoObstacles(newX, newY))
                {
                    if (LastPoint)
                    {
                        if (Delay)
                        {
                            Delay = false;
                        }
                        else
                        {
                            UpdatePalletPosition(newX, newY);
                            PalletToMove.MovementFinished = true;
                            PalletToMove = null;
                            Delay = true;
                            return;
                        }
                    }
                    else
                    {
                        UpdatePalletPosition(newX, newY);
                        PalletToMove.MovementFinished = true;
                        PalletToMove = null;
                        return;
                    }

                }
                else if (scene.StaticObstacle(newX, newY))
                {
                    PalletToMove.MovementFinished = true;
                    return;
                }
            }

            else
            {
                newX += sign;
                if (scene.NoObstacles(newX, newY))
                {
                    UpdatePalletPosition(newX, newY);
                    PalletToMove.MovementFinished = true;
                    PalletToMove = null;
                    return;
                }
                else if (scene.StaticObstacle(newX, newY))
                {
                    PalletToMove.MovementFinished = true;
                    return;
                }
            }
        }


    }
}
