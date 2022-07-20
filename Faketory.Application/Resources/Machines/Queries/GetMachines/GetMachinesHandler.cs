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

namespace Faketory.Application.Resources.Machines.Queries.GetMachines
{
    public class GetMachinesHandler : IRequestHandler<GetMachinesQuery, IEnumerable<Machine>>
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IUserRepository _userRepo;
        public GetMachinesHandler(IMachineRepository machineRepository, IUserRepository userRepo)
        {
            _machineRepository = machineRepository;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Machine>> Handle(GetMachinesQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepo.UserExists(request.UserEmail))
                throw new NotFoundException("This user does not exist!");

            return await _machineRepository.GetAllUserMachines(request.UserEmail);
        }
    }
}
