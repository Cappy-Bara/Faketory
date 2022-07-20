using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Commands.DeleteMachine
{
    public class DeleteMachineHandler : IRequestHandler<DeleteMachineCommand, Unit>
    {
        private readonly IMachineRepository _machineRepo;

        public DeleteMachineHandler(IMachineRepository machineRepo)
        {
            _machineRepo = machineRepo;
        }

        public async Task<Unit> Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
        {
            var machine = await _machineRepo.GetMachine(request.Id);

            if (machine == null)
                throw new NotFoundException("This machine does not exist.");

            if (machine.UserEmail != machine.UserEmail)
                throw new BadRequestException("This machine does not belong to user who made request.");

            await _machineRepo.DeleteMachine(machine);
            return Unit.Value;
        }
    }
}
