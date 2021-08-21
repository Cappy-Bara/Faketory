using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.API.Dtos.Users;
using FluentValidation;

namespace Faketory.API.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Email).NotNull().WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Email).Must(x => !string.IsNullOrEmpty(x)).WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Email).Must(x => x != "").WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Wrong Email address format!").When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password is too short! Use password at least 6 characters long.");

            RuleFor(x => x.RepeatPassword).Must((x, y) => x.Password == x.RepeatPassword).WithMessage("Passwords didn't match!");
        }
    }

    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address cannot be empty!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Wrong Email address format!").When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty!")
                .NotNull().WithMessage("Password cannot be empty!");
        }
    }
}
