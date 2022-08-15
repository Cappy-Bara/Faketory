using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class ConveyorRepository : IConveyorRepository
    {
        private readonly Dictionary<Guid, Conveyor> _conveyors;

        public ConveyorRepository()
        {
            _conveyors = new();
        }

        public Task<List<Conveyor>> GetAllUserConveyors()
        {
            return Task.FromResult(_conveyors.Values.ToList());
        }
        public Task UpdateConveyors(List<Conveyor> conveyors)
        {
            foreach (var conveyor in conveyors)
            {
                _conveyors[conveyor.Id] = conveyor;
            }

            return Task.CompletedTask;
        }
        public Task UpdateConveyor(Conveyor conveyor)
        {
            _conveyors[conveyor.Id] = conveyor;
            return Task.CompletedTask;
        }
        public Task RemoveConveyor(Guid id)
        {
            _conveyors.Remove(id);
            return Task.CompletedTask;
        }
        public Task<Conveyor> GetConveyor(Guid id)
        {
            _conveyors.TryGetValue(id, out var output);

            return Task.FromResult(output);
        }
        public Task<bool> ConveyorExists(Guid id)
        {
            return Task.FromResult(_conveyors.TryGetValue(id,out var _));
        }
        public Task<Guid> AddConveyor(Conveyor conveyor)
        {
            conveyor.Id = Guid.NewGuid();

            _conveyors.Add(conveyor.Id, conveyor);

            return Task.FromResult(conveyor.Id);
        }
    }
}
