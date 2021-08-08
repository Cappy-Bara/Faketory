using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Plc;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreatePlcDtoValidator : AbstractValidator<CreatePlcDto>
    {
        private readonly List<int> ValidModels = new() { 1200, 300, 400, 1500 };


        public CreatePlcDtoValidator()
        {
            RuleFor(p => p.Ip).NotEmpty().WithMessage("IP address cannot be empty!");
            RuleFor(p => p.UserEmail).NotEmpty().WithMessage("Email address cannot be empty!");
            RuleFor(p => p.UserEmail).EmailAddress().WithMessage("You've passed wrong email address!");
            RuleFor(p => p.ModelId).Must(x => ValidModels.Contains(x)).WithMessage("You've chosen wrong PLC model!");
            RuleFor(p => p.SlotId).NotEmpty().WithMessage("You have to chose slot for your PLC!");
        }
    }
}
