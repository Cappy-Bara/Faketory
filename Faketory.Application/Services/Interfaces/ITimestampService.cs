using System.Threading.Tasks;
using Faketory.Domain.Aggregates;

namespace Faketory.Application.Services.Interfaces
{
    public interface ITimestampService
    {
        public Task<ModifiedUtils> Timestamp(string userEmail);

    }
}
