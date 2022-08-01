using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.Enums;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.Resources.IndustrialParts
{
    public class Conveyor
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }
        public int Length { get; set; }
        public bool IsVertical { get; set; }
        public bool IsTurnedDownOrLeft { get; set; }
        public bool IsRunning { get; set; }
        public bool NegativeLogic { get; set; }
        public int Frequency { get; set; }
        public int Ticks { get; set; } = 0;
        public Guid IOId { get; set; }
        public virtual IO IO { get; set; }
        public List<(int, int)> OccupiedPoints { get => GetOccupiedPoints(); }

        private List<(int,int)> GetOccupiedPoints()
        {
            int sign = IsTurnedDownOrLeft ? -1 : 1;
            var output = new List<(int,int)>();
            if (IsVertical)
            {
                for (int i = 0; i < Length; i++)
                    output.Add((PosX, PosY + i * sign));
            }
            else
            {
                for (int i = 0; i < Length; i++)
                    output.Add((PosX + i * sign, PosY));
            }
            return output;
        }
        public bool RefreshStatusAndCheckIfChanged()
        {
            if (IO is null)
                return false;

            var oldState = IsRunning;
            IsRunning = NegativeLogic ? !IO.Value : IO.Value;
            return oldState != IsRunning;
        }
        public List<MovedPallet> MovePallets(List<Pallet> conveyorPallets)
        {
            conveyorPallets ??= new List<Pallet>();
            var output = new List<MovedPallet>();

            if (!IsRunning || Ticks < Frequency)
            {
                conveyorPallets.ForEach(x => output.Add(new MovedPallet(x)));
                Ticks++;
                return output;
            }

            foreach (Pallet pallet in conveyorPallets)
            {
                var movedPallet = new MovedPallet(pallet);

                if (!IsVertical && IsTurnedDownOrLeft)
                    pallet.MoveLeft();
                else if (!IsVertical && !IsTurnedDownOrLeft)
                    pallet.MoveRight();
                else if (IsVertical && !IsTurnedDownOrLeft)
                    pallet.MoveTop();
                else
                    pallet.MoveBottom();

                if (OccupiedPoints.Any(x => x.Item1 == pallet.PosX) &&
                    OccupiedPoints.Any(x => x.Item2 == pallet.PosY))
                {
                    movedPallet.MovePriority = MovePriority.SameConveyor;
                }
                else
                {
                    movedPallet.MovePriority = MovePriority.ChangesConveyor;
                }

                output.Add(movedPallet);
            }
            Ticks = 0;
            return output;
        }
    }
}
