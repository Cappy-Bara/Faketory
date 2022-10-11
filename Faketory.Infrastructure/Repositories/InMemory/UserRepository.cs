using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Faketory.Infrastructure.DbContexts;

namespace Faketory.Infrastructure.Repositories.InMemory   
{
    public class UserRepository : IUserRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public UserRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task AddUser(User user)
        {
            context.Users.Add(user.Email, user);
            await context.Persist();
        }

        public Task<User> GetUser(string email)
        {
            _ = context.Users.TryGetValue(email, out var output);
            return Task.FromResult(output);
        }

        public Task<bool> UserExists(string email)
        {
            return Task.FromResult(context.Users.TryGetValue(email, out var _));
        }
    }
}
