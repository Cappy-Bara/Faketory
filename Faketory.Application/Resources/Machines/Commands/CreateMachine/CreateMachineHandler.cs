using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Commands.CreateMachine
{
    public class CreateMachineHandler : IRequestHandler<CreateMachineCommand, Guid>
    {
        private readonly IMachineRepository _machineRepository;

        public CreateMachineHandler(IMachineRepository machineRepo)
        {
            _machineRepository = machineRepo;
        }

        public async Task<Guid> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
        {
            var userMachines = await _machineRepository.GetAllUserMachines();
            var existingMachine = userMachines.FirstOrDefault(x => x.PosX == request.PosX & x.PosY == request.PosY);
            if (existingMachine != null)
                throw new OccupiedException("This position is already occupied by another machine");

            var machine = new Machine(request.PosX, request.PosY, request.ProcessingTimestampAmount, request.RandomFactor);
            var id = await _machineRepository.AddMachine(machine);

            return id;
        }
    }
}
