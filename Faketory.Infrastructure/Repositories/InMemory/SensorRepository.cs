using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class SensorRepository : ISensorRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public SensorRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task AddSensor(Sensor sensor)
        {
            sensor.Id = Guid.NewGuid();

            context.Sensors.Add(sensor.Id,sensor);
            await context.Persist();
        }
        public Task<Sensor> GetSensorById(Guid sensorId)
        {
            context.Sensors.TryGetValue(sensorId,out var output);
            return Task.FromResult(output);
        }
        public Task<List<Sensor>> GetUserSensors()
        {
            return Task.FromResult(context.Sensors.Values.ToList());
        }
        public async Task RemoveSensor(Guid sensorId)
        {
            context.Sensors.Remove(sensorId);
            await context.Persist();
        }
        public Task<bool> SensorExist(int posX, int posY)
        {
            return Task.FromResult(context.Sensors.Any(x => x.Value.PosX == posX && x.Value.PosY == posY));
        }
        public Task<bool> SensorExist(Guid id)
        {
            return Task.FromResult(context.Sensors.Any(x => x.Value.Id == id));
        }
        public async Task UpdateSensor(Sensor sensor)
        {
            context.Sensors[sensor.Id] = sensor;
            await context.Persist();
        }
        public async Task UpdateSensors(List<Sensor> sensors)
        {
            foreach (var sensor in sensors)
            {
                context.Sensors[sensor.Id] = sensor;
            }

            await context.Persist();
        }
        public Task<bool> IOOccupiedBySensor(Guid IoId, Guid sensorId)
        {
            return Task.FromResult(context.Sensors.Values.Any(x => x.IOId == IoId && x.Id != sensorId));
        }
    }
}
