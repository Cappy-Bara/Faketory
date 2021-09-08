using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Conveyors.Requests;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreateConveyorValidator : AbstractValidator<CreateConveyorDto>
    {
        public CreateConveyorValidator()
        {
            RuleFor(x => x.Bit).InclusiveBetween(0, 7).WithMessage("Bit must be higher than 0 and lower than 7!");
            RuleFor(x => x.Bit).NotEmpty().WithMessage("Bit cannot be empty");

            RuleFor(x => x.Byte).GreaterThanOrEqualTo(0).WithMessage("Byte cannot be lower than 0");
            RuleFor(x => x.Byte).NotEmpty().WithMessage("Byte cannot be empty");

            RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0).WithMessage("Frequency cannot be lower than 0!");
            RuleFor(x => x.Frequency).NotEmpty().WithMessage("Frequency cannot be empty");

            RuleFor(x => x.IsTurnedDownOrLeft).NotEmpty().WithMessage("You have to choose conveyor orientation");
            RuleFor(x => x.IsVertical).NotEmpty().WithMessage("You have to choose conveyor orientation");

            RuleFor(x => x.Length).NotEmpty().WithMessage("You have to choose conveyor length!");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Length must be higher than 0");

            RuleFor(x => x.PosX).NotEmpty().WithMessage("Coordinates cannot be empty");
            RuleFor(x => x.PosY).NotEmpty().WithMessage("Coordinates cannot be empty");

            RuleFor(x => x.SlotId).NotEmpty().WithMessage("You have to choose Slot");
        }
    }
    public class RemoveConveyorValidator : AbstractValidator<RemoveConveyorDto>
    {
        public RemoveConveyorValidator()
        {
            RuleFor(x => x.ConveyorId).NotEmpty().WithMessage("Conveyor Id cannot be empty");
        }
    }
    public class GetConveyorValidator : AbstractValidator<GetConveyorDto>
    {
        public GetConveyorValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Conveyor Id cannot be empty");
        }
    }
    public class UpdateConveyorValidator : AbstractValidator<UpdateConveyorDto>
    {
        public UpdateConveyorValidator()
        {
            RuleFor(x => x.ConveyorId).NotEmpty().WithMessage("Conveyor Id cannot be empty!");

            RuleFor(x => x.Bit).InclusiveBetween(0, 7).WithMessage("Bit must be higher than 0 and lower than 7!");
            RuleFor(x => x.Bit).NotEmpty().WithMessage("Bit cannot be empty");

            RuleFor(x => x.Byte).GreaterThanOrEqualTo(0).WithMessage("Byte cannot be lower than 0");
            RuleFor(x => x.Byte).NotEmpty().WithMessage("Byte cannot be empty");

            RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0).WithMessage("Frequency cannot be lower than 0!");
            RuleFor(x => x.Frequency).NotEmpty().WithMessage("Frequency cannot be empty");

            RuleFor(x => x.IsTurnedDownOrLeft).NotEmpty().WithMessage("You have to choose conveyor orientation");
            RuleFor(x => x.IsVertical).NotEmpty().WithMessage("You have to choose conveyor orientation");

            RuleFor(x => x.Length).NotEmpty().WithMessage("You have to choose conveyor length!");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Length must be higher than 0");

            RuleFor(x => x.PosX).NotEmpty().WithMessage("Coordinates cannot be empty");
            RuleFor(x => x.PosY).NotEmpty().WithMessage("Coordinates cannot be empty");

            RuleFor(x => x.SlotId).NotEmpty().WithMessage("You have to choose Slot");
        }
    }
}
