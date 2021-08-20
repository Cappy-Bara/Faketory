using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Plc.Requests;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class CreatePlcDtoValidator : AbstractValidator<CreatePlcDto>
    {
        private readonly List<int> ValidModels = new() { 1200, 300, 400, 1500 };
        public CreatePlcDtoValidator()
        {
            RuleFor(p => p.Ip).NotEmpty().WithMessage("IP address cannot be empty!")
                .Must(p => IPAddress.TryParse(p,out _)).WithMessage("Wrong IP Address!");
            RuleFor(p => p.UserEmail).NotEmpty().WithMessage("Email address cannot be empty!");
            RuleFor(p => p.UserEmail).EmailAddress().WithMessage("You've passed wrong email address!")
                .When(x => (!string.IsNullOrEmpty(x.UserEmail)));
            RuleFor(p => p.ModelId).Must(x => ValidModels.Contains(x)).WithMessage("You've chosen wrong PLC model!");
        }
    }

    public class GetUserPlcsRequestDtoValidator : AbstractValidator<GetUserPlcsRequestDto>
    {
        public GetUserPlcsRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You've passed wrong email address.")
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }

    public class DeletePlcRequestDtoValidator : AbstractValidator<DeletePlcRequestDto>
    {
        public DeletePlcRequestDtoValidator()
        {
            RuleFor(x => x.PlcId).NotEmpty().WithMessage("ID cannot be empty!");
        }
    }

    public class GetConnectionsRequestDtoValidator : AbstractValidator<GetConnectionsRequestDto>
    {
        public GetConnectionsRequestDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("You've passed wrong email address.")
                .When(x => !string.IsNullOrEmpty(x.Email));
        }
    }
}
