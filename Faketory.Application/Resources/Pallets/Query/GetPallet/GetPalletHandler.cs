using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Query.GetPallet
{
    public class GetPalletHandler : IRequestHandler<GetPalletQuery, Pallet>
    {
        private readonly IPalletRepository _palletRepo;

        public GetPalletHandler(IPalletRepository palletRepo)
        {
            _palletRepo = palletRepo;
        }

        public async Task<Pallet> Handle(GetPalletQuery request, CancellationToken cancellationToken)
        {
            return await _palletRepo.GetPallet(request.PalletId);
        }
    }
}
