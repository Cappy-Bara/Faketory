using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly FaketoryDbContext _dbContext;

        public SensorRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddSensor(Sensor sensor)
        {
            _dbContext.Sensors.Add(sensor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Sensor> GetSensorById(Guid sensorId)
        {
            return await _dbContext.Sensors.FirstOrDefaultAsync(x => x.Id == sensorId);
        }
        public async Task<List<Sensor>> GetUserSensors(string userEmail)
        {
           return await _dbContext.Sensors.Where(x => x.UserEmail == userEmail).Include(x => x.IO).ToListAsync();
        }
        public async Task RemoveSensor(Guid sensorId)
        {
            var sensor = await GetSensorById(sensorId);
            _dbContext.Sensors.Remove(sensor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> SensorExist(int posX, int posY, string userEmail)
        {
            return await _dbContext.Sensors.AnyAsync(x => x.UserEmail == userEmail && x.PosX == posX && x.PosY == posY);
        }
        public async Task<bool> SensorExist(Guid id)
        {
            return await _dbContext.Sensors.AnyAsync(x => x.Id == id);
        }
        public async Task UpdateSensor(Sensor sensor)
        {
            _dbContext.Sensors.Update(sensor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateSensors(List<Sensor> sensors)
        {
            _dbContext.Sensors.UpdateRange(sensors);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> IOOccupiedBySensor(Guid IoId, Guid sensorId)
        {
            return await _dbContext.Sensors.AnyAsync(x => x.IOId == IoId && x.Id != sensorId);
        }
    }
}
