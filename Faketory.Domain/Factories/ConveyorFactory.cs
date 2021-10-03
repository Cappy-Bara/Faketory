using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Factories
{
    public class ConveyorFactory
    {
        private readonly IConveyingPointRepository _conveyingPointRepo;

        public ConveyorFactory(IConveyingPointRepository conveyingPointRepo)
        {
            _conveyingPointRepo = conveyingPointRepo;
        }


        public async Task<Conveyor> CreateConveyor(int posX, int posY, int length, int frequency, bool isVertical,
            bool isTurnedDownOrLeft, string userEmail, Guid ioId, bool negativeLogic)
        {
            var conveyor = new Conveyor()
            {
                Frequency = frequency,
                PosX = posX,
                PosY = posY,
                Length = length,
                IsVertical = isVertical,
                IsTurnedDownOrLeft = isTurnedDownOrLeft,
                UserEmail = userEmail,
                IOId = ioId,
                NegativeLogic = negativeLogic
            };

            conveyor.ConveyingPoints = GetConveyingPoints(conveyor);
            await ExceptionIfCollides(conveyor);

            return conveyor;
        }
        public async Task<Conveyor> UpdateConveyor(Conveyor c, int posX, int posY, int length, int frequency, bool isVertical,
            bool isTurnedDownOrLeft, Guid ioId, bool negativeLogic) 
        {
            var cpWillChange = ConveyorPointsWillChange(c, posX, posY, isVertical, isTurnedDownOrLeft, length);

            c.Frequency = frequency;
            c.PosX = posX;
            c.PosY = posY;
            c.Length = length;
            c.IsVertical = isVertical;
            c.IsTurnedDownOrLeft = isTurnedDownOrLeft;
            c.IOId = ioId;
            c.NegativeLogic = negativeLogic;

            if (cpWillChange)
            {
                c.ConveyingPoints = GetConveyingPoints(c);
                await ExceptionIfCollides(c);
            }
            return c;
        }

        private async Task ExceptionIfCollides(Conveyor conveyor)
        {
            if (await ConveyorCollides(conveyor))
            {
                throw new NotCreatedException("Conveyor collides with another one!");
            }
        }
        private async Task<bool> ConveyorCollides(Conveyor c)
        {
            var ExistingPoints = await _conveyingPointRepo.GetAllUserConveyingPoints(c.UserEmail);
            return ExistingPoints.Any(x => x.ConveyorId != c.Id && (c.ConveyingPoints.Any(z => (z.PosX == x.PosX) && (z.PosY == x.PosY))));
        }
        private List<ConveyingPoint> GetConveyingPoints(Conveyor conveyor)
        {
            int sign = conveyor.IsTurnedDownOrLeft ? -1 : 1;
            var output = new List<ConveyingPoint>();
            if (conveyor.IsVertical)
            {
                for (int i = 0; i < conveyor.Length - 1; i++)
                    output.Add(new ConveyingPoint(conveyor.PosX, conveyor.PosY + i * sign));
                output.Add(new ConveyingPoint(conveyor.PosX, conveyor.PosY + (conveyor.Length - 1) * sign)
                {
                    LastPoint = true
                });
            }
            else
            {
                for (int i = 0; i < conveyor.Length - 1; i++)
                    output.Add(new ConveyingPoint(conveyor.PosX + i * sign, conveyor.PosY));
                output.Add(new ConveyingPoint(conveyor.PosX + (conveyor.Length - 1) * sign, conveyor.PosY)
                {
                    LastPoint = true
                });
            }
            return output;
        }
        public bool ConveyorPointsWillChange(Conveyor c, int posX, int posY, bool isVertical, bool isTurnedDownOrLeft, int length)
        {
            return (c.PosX != posX) || (c.PosY != posY) || (c.IsVertical != isVertical) ||
            (c.IsTurnedDownOrLeft != isTurnedDownOrLeft) || (c.Length != length);
        }
    }
}
