using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Faketory.Domain.Exceptions;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Faketory.Application.Resources.Users.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _hasher;

        public RegisterUserHandler(IUserRepository userRepo, IPasswordHasher<User> hasher)
        {
            _userRepo = userRepo;
            _hasher = hasher;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepo.UserExists(request.Email))
                throw new ResourceExistException("User with this email already exist!");

            var user = new User()
            {
                Email = request.Email,
            };

            var passwordHash = _hasher.HashPassword(user, request.Password);
            user.PasswordHash = passwordHash;

            await _userRepo.AddUser(user);

            return Unit.Value;
        }
    }
}
