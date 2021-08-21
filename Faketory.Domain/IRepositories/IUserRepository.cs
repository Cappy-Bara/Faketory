using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;

namespace Faketory.Domain.IRepositories
{
    public interface IUserRepository
    {
        public Task<bool> UserExists(string email);
        public Task AddUser(User user);
        public Task<User> GetUser(string email);
    }
}
