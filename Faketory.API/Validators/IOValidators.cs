using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.IOs.Requests;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreateIOValidator : AbstractValidator<CreateIODto>
    {
        public CreateIOValidator()
        {
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid I/O Type!")
                .NotNull().WithMessage("Wrong IO Type!");
            RuleFor(x => x.Bit).NotNull().WithMessage("Invalid Bit address!")
                .Must(x => x >= 0).WithMessage("Invalid Bit address!")
                .LessThanOrEqualTo(7).WithMessage("Bit must be lower than 7!");
            RuleFor(x => x.Byte).NotNull().WithMessage("Invalid Byte address!")
                .Must(x => x >= 0).WithMessage("Invalid Byte address!");
        }
    }





}
