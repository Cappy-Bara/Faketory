using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;
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

            await _machineRepo.DeleteMachine(machine);
            return Unit.Value;
        }
    }
}
