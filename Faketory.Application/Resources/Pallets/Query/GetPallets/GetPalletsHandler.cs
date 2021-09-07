using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Query.GetPallets
{
    public class GetPalletsHandler : IRequestHandler<GetPalletsQuery, List<Pallet>>
    {
        private readonly IPalletRepository _palletRepo;

        public GetPalletsHandler(IPalletRepository palletRepo)
        {
            _palletRepo = palletRepo;
        }

        public async Task<List<Pallet>> Handle(GetPalletsQuery request, CancellationToken cancellationToken)
        {
            return await _palletRepo.GetAllUserPallets(request.UserEmail);
        }
    }
}
