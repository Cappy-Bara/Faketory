using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.UpdatePallet
{
    public class UpdatePalletHandler : IRequestHandler<UpdatePalletQuery, Unit>
    {
        private readonly IPalletRepository _palletRepo;

        public UpdatePalletHandler(IPalletRepository palletRepo)
        {
            _palletRepo = palletRepo;
        }


        public async Task<Unit> Handle(UpdatePalletQuery request, CancellationToken cancellationToken)
        {
            var pallet = await _palletRepo.GetPallet(request.PalletId);

            pallet.PosX = request.PosX;
            pallet.PosY = request.PosY;

            await _palletRepo.UpdatePallet(pallet);
            return Unit.Value;
        }
    }
}
