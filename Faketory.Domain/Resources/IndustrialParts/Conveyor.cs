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
        public int Frequency { get; set; }     //ZABEZPIECZYĆ -> wieksze niz 0
        public int Ticks { get; set; } = 0;
        public virtual List<ConveyingPoint> ConveyingPoints { get; set; } = new List<ConveyingPoint>();
        public Guid IOId { get; set; }
        public virtual IO IO { get; set; } 

        public Conveyor(int posX, int posY, int length, int frequency, bool isVertical, bool isTurnedDownOrLeft, string userEmail)
        {
            Frequency = frequency;
            PosX = posX;
            PosY = posY;
            Length = length;
            IsVertical = isVertical;
            IsTurnedDownOrLeft = isTurnedDownOrLeft;
            UserEmail = userEmail;

            ConveyingPoints = GetConveyingPoints();
        }
        public Conveyor()
        {
            ;
        }
        private List<ConveyingPoint> GetConveyingPoints()
        {
            int sign = IsTurnedDownOrLeft ? -1 : 1;
            var output = new List<ConveyingPoint>();
            if (IsVertical)
            {
                for (int i = 0; i < Length - 1; i++)
                    output.Add(new ConveyingPoint(PosX, PosY + i * sign));
                output.Add(new ConveyingPoint(PosX, PosY + (Length - 1) * sign)
                {
                    LastPoint = true
                });
            }
            else
            {
                for (int i = 0; i < Length - 1; i++)
                    output.Add(new ConveyingPoint(PosX + i * sign, PosY));
                output.Add(new ConveyingPoint(PosX + (Length - 1) * sign, PosY)
                {
                    LastPoint = true
                });
            }
            return output;
        }
        public void RefreshConveyorStatus()
        {
            IsRunning = IO.Value;
        }
        public void MovePallets(Scene scene)
        {
            if (Ticks >= Frequency)
            {
                foreach (ConveyingPoint cp in ConveyingPoints)
                    cp.MovePalletAtPoint(IsVertical, IsTurnedDownOrLeft, scene);
                Ticks = 0;
            }
            else
            {
                foreach (ConveyingPoint cp in ConveyingPoints)
                    cp.MarkPalletAsMoved();
                Ticks++;
            }
        }
    }
}
