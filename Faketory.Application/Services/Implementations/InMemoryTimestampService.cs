using Faketory.Application.Services.Interfaces;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.PLCRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Application.Services.Implementations
{
    public class InMemoryTimestampService : TimestampService, ITimestampService
    {
        private readonly IIORepository _ioRepo;

        public InMemoryTimestampService(
            IMediator mediator,
            IPalletRepository palletRepo,
            ISensorRepository sensorRepo,
            IConveyorRepository conveyorRepo,
            IMachineRepository machinesRepo,
            IIORepository ioRepo) : base(mediator, palletRepo, sensorRepo, conveyorRepo, machinesRepo)
        {
            _ioRepo = ioRepo;
        }

        protected override async Task DataReading()
        {
            await base.DataReading();

            var ios = await _ioRepo.GetIOs();

            _userUtils.Sensors = _userUtils.Sensors.Join(ios, sensor => sensor.IOId, io => io.Id, (x, y) => { x.IO = y; return x; }).ToList();
            _userUtils.Conveyors = _userUtils.Conveyors.Join(ios, conveyor => conveyor.IOId, io => io.Id, (x, y) => { x.IO = y; return x; }).ToList();
        }
    }
}
