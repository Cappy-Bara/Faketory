using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.IRepositories
{
    public interface ISensorRepository
    {
        public Task AddSensor(Sensor sensor);
        public Task<List<Sensor>> GetUserSensors();
        public Task<Sensor> GetSensorById(Guid sensorId);
        public Task RemoveSensor(Guid sensorId);
        public Task UpdateSensor(Sensor sensor);
        public Task UpdateSensors(List<Sensor> sensors);
        public Task<bool> SensorExist(int posX, int posY);
        public Task<bool> SensorExist(Guid id);
        public Task<bool> IOOccupiedBySensor(Guid IoId, Guid sensorId);
    }
}
