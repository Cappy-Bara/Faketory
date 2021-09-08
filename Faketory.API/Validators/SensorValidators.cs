using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Sensors.Reqests;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreateSensorValidators : AbstractValidator<CreateSensorDto>
    {
        public CreateSensorValidators()
        {
            RuleFor(x => x.Bit).InclusiveBetween(0, 7).WithMessage("Bit must be higher than 0 and lower than 7!");
            RuleFor(x => x.Bit).NotEmpty().WithMessage("Bit cannot be empty");

            RuleFor(x => x.Byte).GreaterThanOrEqualTo(0).WithMessage("Byte cannot be lower than 0");
            RuleFor(x => x.Byte).NotEmpty().WithMessage("Byte cannot be empty");

            RuleFor(x => x.PosX).NotEmpty().WithMessage("Coordinates cannot be empty");
            RuleFor(x => x.PosY).NotEmpty().WithMessage("Coordinates cannot be empty");

            RuleFor(x => x.SlotId).NotEmpty().WithMessage("You have to choose Slot");
        }
    }
    public class RemoveSensorValidators : AbstractValidator<RemoveSensorDto>
    {
        public RemoveSensorValidators()
        {
            RuleFor(x => x.SensorId).NotEmpty().WithMessage("Sensor Id cannot be empty");
        }
    }
    public class GetSensorValidator : AbstractValidator<GetSensorDto>
    {
        public GetSensorValidator()
        {
            RuleFor(x => x.SensorId).NotEmpty().WithMessage("Sensor Id cannot be empty");
        }
    }

    public class UpdateSensorValidator : AbstractValidator<UpdateSensorDto>
    {
        public UpdateSensorValidator()
        {
            RuleFor(x => x.SensorId).NotEmpty().WithMessage("Sensor Id cannot be empty!");

            RuleFor(x => x.Bit).InclusiveBetween(0, 7).WithMessage("Bit must be higher than 0 and lower than 7!");
            RuleFor(x => x.Bit).NotEmpty().WithMessage("Bit cannot be empty");

            RuleFor(x => x.Byte).GreaterThanOrEqualTo(0).WithMessage("Byte cannot be lower than 0");
            RuleFor(x => x.Byte).NotEmpty().WithMessage("Byte cannot be empty");

            RuleFor(x => x.PosX).NotEmpty().WithMessage("Coordinates cannot be empty");
            RuleFor(x => x.PosY).NotEmpty().WithMessage("Coordinates cannot be empty");

            RuleFor(x => x.SlotId).NotEmpty().WithMessage("You have to choose Slot");
        }
    }
}
