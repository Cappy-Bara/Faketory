using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
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
            var pallet = await _palletRepo.GetPallet(request.PalletId,request.UserEmail);
            if(pallet.UserEmail != request.UserEmail)
                throw new BadRequestException("This sensor is not yours!");

            pallet.PosX = request.PosX;
            pallet.PosY = request.PosY;

            await _palletRepo.UpdatePallet(pallet);
            return Unit.Value;
        }
    }
}
