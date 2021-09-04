using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using MediatR;

namespace Faketory.Application.Resources.Pallets.Commands.RemovePallet
{
    public class RemovePalletHandler : IRequestHandler<RemovePalletCommand, Unit>
    {
        private readonly IPalletRepository _palletRepo;

        public RemovePalletHandler(IPalletRepository palletRepo)
        {
            _palletRepo = palletRepo;
        }

        public async Task<Unit> Handle(RemovePalletCommand request, CancellationToken cancellationToken)
        {
            var pallet = await _palletRepo.GetPallet(request.PalletId, request.UserEmail);

            if (pallet is null)
                throw new NotFoundException("This pallet does not exist");

            await _palletRepo.RemovePallet(pallet);

            return Unit.Value;
        }
    }
}
