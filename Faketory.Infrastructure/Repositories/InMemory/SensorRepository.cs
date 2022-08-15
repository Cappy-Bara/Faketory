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
        private readonly Dictionary<Guid, Sensor> _sensors;
        public SensorRepository()
        {
            _sensors = new Dictionary<Guid, Sensor>();
        }
        public Task AddSensor(Sensor sensor)
        {
            sensor.Id = Guid.NewGuid();

            _sensors.Add(sensor.Id,sensor);
            return Task.CompletedTask;
        }
        public Task<Sensor> GetSensorById(Guid sensorId)
        {
            _sensors.TryGetValue(sensorId,out var output);
            return Task.FromResult(output);
        }
        public Task<List<Sensor>> GetUserSensors()
        {
            return Task.FromResult(_sensors.Values.ToList());
        }
        public Task RemoveSensor(Guid sensorId)
        {
            _sensors.Remove(sensorId);
            return Task.CompletedTask;
        }
        public Task<bool> SensorExist(int posX, int posY)
        {
            return Task.FromResult(_sensors.Any(x => x.Value.PosX == posX && x.Value.PosY == posY));
        }
        public Task<bool> SensorExist(Guid id)
        {
            return Task.FromResult(_sensors.Any(x => x.Value.Id == id));
        }
        public Task UpdateSensor(Sensor sensor)
        {
            _sensors[sensor.Id] = sensor;
            return Task.CompletedTask;
        }
        public Task UpdateSensors(List<Sensor> sensors)
        {
            foreach (var sensor in sensors)
            {
                _sensors[sensor.Id] = sensor;
            }

            return Task.CompletedTask;
        }
        public Task<bool> IOOccupiedBySensor(Guid IoId, Guid sensorId)
        {
            return Task.FromResult(_sensors.Values.Any(x => x.IOId == IoId && x.Id != sensorId));
        }
    }
}
