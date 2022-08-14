using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.CreatePallet
{
    public class CreatePalletHandler : IRequestHandler<CreatePalletCommand, bool>
    {
        private readonly IPalletRepository _palletRepository;

        public CreatePalletHandler(IPalletRepository palletRepository)
        {
            _palletRepository = palletRepository;
        }

        public async Task<bool> Handle(CreatePalletCommand request, CancellationToken cancellationToken)
        {
            if (await _palletRepository.PalletCollides(request.PosX, request.PosY))
            {
                throw new NotCreatedException("Pallet collides with something different.");
            }

            var pallet = new Pallet(request.PosX, request.PosY);

            await _palletRepository.AddPallet(pallet);

            return true;
        }
    }
}
