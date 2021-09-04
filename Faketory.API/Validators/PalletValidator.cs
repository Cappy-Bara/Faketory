using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Pallets.Requests;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreatePalletValidator : AbstractValidator<CreatePalletDto>
    {
        public CreatePalletValidator()
        {
            RuleFor(x => x.PosX).NotEmpty().WithMessage("X position cannot be empty!");
            RuleFor(x => x.PosY).NotEmpty().WithMessage("Y position cannot be empty!");
        }
    }

    public class RemovePalletValidator : AbstractValidator<RemovePalletDto>
    {
        public RemovePalletValidator()
        {
            RuleFor(x => x.PalletId).NotEmpty().WithMessage("Pallet Id cannot be empty!");
        }
    }

    public class GetPalletValidator : AbstractValidator<GetPalletDto>
    {
        public GetPalletValidator()
        {
            RuleFor(x => x.PalletId).NotEmpty().WithMessage("Pallet Id cannot be empty!");
        }
    }
}
