using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Faketory.Application.Resources.Users.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, List<Claim>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _hasher;

        public LoginUserHandler(IPasswordHasher<User> hasher, IUserRepository userRepo)
        {
            _hasher = hasher;
            _userRepo = userRepo;
        }

        public async Task<List<Claim>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
           var user = await _userRepo.GetUser(request.Email);

            if (user == null)
                throw new NotFoundException("User with this email does not exist!");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid password!");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
    }
}
