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
        public GetMachinesHandler(IMachineRepository machineRepository, IUserRepository userRepo)
        {
            _machineRepository = machineRepository;
        }

        public async Task<IEnumerable<Machine>> Handle(GetMachinesQuery request, CancellationToken cancellationToken)
        {
            return await _machineRepository.GetAllUserMachines();
        }
    }
}
