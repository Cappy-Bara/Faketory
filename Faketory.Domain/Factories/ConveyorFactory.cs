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
        private readonly IConveyorRepository _conveyorRepository;
        private readonly string _email;
        public ConveyorFactory(IConveyorRepository conveyorRepository, string email)
        {
            _conveyorRepository = conveyorRepository;
            _email = email;
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
                await ExceptionIfCollides(c);

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
            var points = new List<(int, int, bool)>();
            var conveyors = await _conveyorRepository.GetAllUserConveyors(_email);
            conveyors = conveyors.Where(x => x.Id != c.Id).ToList();
            conveyors.ForEach(c => points.AddRange(c.OccupiedPoints));

            return points.Any(x => c.OccupiedPoints.Any(z => (z.Item1 == x.Item1) && (z.Item2 == x.Item2)));
        }
        private static bool ConveyorPointsWillChange(Conveyor c, int posX, int posY, bool isVertical, bool isTurnedDownOrLeft, int length)
        {
            return (c.PosX != posX) || (c.PosY != posY) || (c.IsVertical != isVertical) ||
            (c.IsTurnedDownOrLeft != isTurnedDownOrLeft) || (c.Length != length);
        }
    }
}
