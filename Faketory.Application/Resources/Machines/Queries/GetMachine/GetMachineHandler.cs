using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Faketory.Application.Resources.Machines.Queries.GetMachine
{
    public class GetMachineHandler : IRequestHandler<GetMachineQuery, Machine>
    {
        private readonly IMachineRepository _machineRepo;

        public GetMachineHandler(IMachineRepository machineRepo)
        {
            _machineRepo = machineRepo;
        }

        public async Task<Machine> Handle(GetMachineQuery request, CancellationToken cancellationToken)
        {
            var machine = await _machineRepo.GetMachine(request.MachineId);

            if (machine is null)
                throw new NotFoundException("Machine with this ID does not exist.");

            if (machine.UserEmail != request.UserEmail)
                throw new BadRequestException("This machine doesn't belong to user which sent the request.");

            return machine;
        }
    }
}
