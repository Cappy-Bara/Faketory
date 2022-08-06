using Faketory.Common;
using Faketory.Common.TimeMeasuring;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Faketory.Infrastructure.Repositories.TimeMeasuring
{
    public class PlcTimeMeasuringRepository : IActivable, IPlcRepository
    {
        private readonly IPlcRepository _plcRepo;
        private readonly MethodTimeMeasurer<PlcTimeMeasuringRepository> methodTimeMesurer;
        public string ConfigurationKey => "MeasurePlcRepositoryTimes";

        public PlcTimeMeasuringRepository(IPlcRepository plcRepo, IServiceScopeFactory scopeFactory)
        {
            _plcRepo = plcRepo;
            methodTimeMesurer = new(scopeFactory);
        }
        public PlcTimeMeasuringRepository()
        {

        }


        public async Task<bool> ConnectToPlc(Guid id)
        {
            Func<Task<bool>> repoFunction = async () => (await _plcRepo.ConnectToPlc(id));
            return await methodTimeMesurer.MeasureTime(repoFunction);
        }

        public void CreatePlc(PlcEntity entity)
        {
            var repoFunction = new Action(() => _plcRepo.CreatePlc(entity));
            methodTimeMesurer.MeasureTime(repoFunction);
        }

        public void DeletePlc(Guid id)
        {
            var repoFunction = new Action(() => _plcRepo.DeletePlc(id));
            methodTimeMesurer.MeasureTime(repoFunction);
        }

        public bool IsConnected(Guid id)
        {
            var repoFunction = new Func<bool>(() => _plcRepo.IsConnected(id));
            return methodTimeMesurer.MeasureTime(repoFunction);
        }

        public bool PlcExists(Guid id)
        {
            var repoFunction = new Func<bool>(() => _plcRepo.PlcExists(id));
            return methodTimeMesurer.MeasureTime(repoFunction);
        }

        public async Task<bool> ReadFromPlc(Guid id, int @byte, int bit)
        {
            var repoFunction = new Func<Task<bool>>(async () => await _plcRepo.ReadFromPlc(id, @byte, bit));
            return await methodTimeMesurer.MeasureTime(repoFunction);
        }

        public async Task WriteToPlc(Guid id, int @byte, int bit, bool value)
        {
            var repoFunction = new Func<Task>(async () => await _plcRepo.WriteToPlc(id,@byte,bit,value));
            await methodTimeMesurer.MeasureTime(repoFunction);
        }
    }
}
