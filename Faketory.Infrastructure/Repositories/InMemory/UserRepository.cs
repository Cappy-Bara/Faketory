using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory   
{
    public class UserRepository : IUserRepository
    {
        private readonly Dictionary<string,User> _users;

        public UserRepository()
        {
            _users = new Dictionary<string, User>();
        }

        public Task AddUser(User user)
        {
            _users.Add(user.Email, user);
            return Task.CompletedTask;
        }

        public Task<User> GetUser(string email)
        {
            _ = _users.TryGetValue(email, out var output);
            return Task.FromResult(output);
        }

        public Task<bool> UserExists(string email)
        {
            return Task.FromResult(_users.TryGetValue(email, out var _));
        }
    }
}
