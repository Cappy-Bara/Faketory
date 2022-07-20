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

namespace Faketory.Application.Resources.Machines.Commands.CreateMachine
{
    public class CreateMachineHandler : IRequestHandler<CreateMachineCommand, Guid>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IUserRepository _userRepo;

        public CreateMachineHandler(IMachineRepository machineRepo, IUserRepository userRepo)
        {
            _machineRepository = machineRepo;
            _userRepo = userRepo;
        }

        public async Task<Guid> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("User does not exist");

            var userMachines = await _machineRepository.GetAllUserMachines(request.UserEmail);
            var existingMachine = userMachines.FirstOrDefault(x => x.PosX == request.PosX & x.PosY == request.PosY);
            if (existingMachine != null)
                throw new OccupiedException("This position is already occupied by another machine");

            var machine = new Machine(request.PosX, request.PosY, request.UserEmail, request.ProcessingTimestampAmount, request.RandomFactor);
            var id = await _machineRepository.AddMachine(machine);

            return id;
        }
    }
}
