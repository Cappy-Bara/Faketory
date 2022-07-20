using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Commands.UpdateMachine
{
    public class UpdateMachineHandler : IRequestHandler<UpdateMachineCommand, Unit>
    {
        private readonly IMachineRepository _machineRepo;
        public UpdateMachineHandler(IMachineRepository machineRepo)
        {
            _machineRepo = machineRepo;
        }

        public async Task<Unit> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
        {
            var machine = await _machineRepo.GetMachine(request.MachineId);

            if (machine == null)
                throw new NotFoundException("This machine does not exists.");

            if (machine.UserEmail != request.UserEmail)
                throw new BadRequestException("This machine does not belong to chosen user.");

            var machines = await _machineRepo.GetAllUserMachines(request.UserEmail);

            var colidingMachine = machines.FirstOrDefault(x => x.PosX == request.PosX & x.PosY == request.PosY);
            if (colidingMachine != null)
                throw new OccupiedException("Machine collides with different machine.");

            machine.PosX = request.PosX;
            machine.PosY = request.PosY;
            machine.ProcessingTimestampAmount = request.ProcessingTimestampAmount;
            machine.RandomFactor = request.RandomFactor;

            await _machineRepo.UpdateMachine(machine);
            return Unit.Value;
        }
    }
}
