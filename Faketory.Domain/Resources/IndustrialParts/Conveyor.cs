using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
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
        public List<(int, int, bool)> OccupiedPoints { get => GetConveyingPoints(); }

        public Conveyor(int posX, int posY, int length, int frequency, bool isVertical, bool isTurnedDownOrLeft, string userEmail)
        {
            Frequency = frequency;
            PosX = posX;
            PosY = posY;
            Length = length;
            IsVertical = isVertical;
            IsTurnedDownOrLeft = isTurnedDownOrLeft;
            UserEmail = userEmail;
        }
        public Conveyor()
        {
            ;
        }
        private List<(int,int,bool)> GetConveyingPoints()
        {
            int sign = IsTurnedDownOrLeft ? -1 : 1;
            var output = new List<(int,int,bool)>();
            if (IsVertical)
            {
                for (int i = 0; i < Length - 1; i++)
                    output.Add((PosX, PosY + i * sign,false));
                output.Add((PosX, PosY + (Length - 1) * sign, true));
            }
            else
            {
                for (int i = 0; i < Length - 1; i++)
                    output.Add((PosX + i * sign, PosY,false));
                output.Add((PosX + (Length - 1) * sign, PosY, true));
            }
            return output;
        }
        public void RefreshConveyorStatus()
        {
            IsRunning = NegativeLogic ? !IO.Value : IO.Value;
        }
    }
}
