using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Aggregates;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Application.Services
{
    public class SceneHandler
    {

        //mediator request handler który wywoła timestamp

        private readonly IConveyorRepository _conveyorRepo;
        private readonly IConveyingPointRepository _conveyorPointRepo;
        private readonly IPalletRepository _palletRepo;
        private readonly ISensorRepository _sensorRepo;
        private Scene scene;
        private string _email;

        public SceneHandler(IPalletRepository palletRepo, IConveyorRepository conveyorRepo,IConveyingPointRepository conveyorPointRepo,ISensorRepository sensorRepo, string email)
        {
            _palletRepo = palletRepo;
            _conveyorRepo = conveyorRepo;
            _conveyorPointRepo = conveyorPointRepo;
            scene = new Scene(email, conveyorRepo, palletRepo);
            _sensorRepo = sensorRepo;
            _email = email;
        }
        public async Task Timestamp()
        {
            await scene.CreateScene();
            
            scene.UpdateConveyorsStatus();
            await scene.BindPalletToPoint();
            scene.MarkStaticBlocksAsMoved();
            MoveBlocks();

            var sensors = await _sensorRepo.GetUserSensors(_email);
            sensors.ForEach(x => {
                x.Sense(scene);
                x.RefreshIOState();
            });

            await UpdateInDb(sensors);
        }
        public void MoveBlocks()
        {
            while (scene.Pallets.Any(x => !x.MovementFinished))
            {
                foreach (Conveyor c in scene.Conveyors)
                {
                    c.MovePallets(scene);
                }
            }
            foreach (Pallet b in scene.Pallets)
                b.MovementFinished = false;
        }
        public async Task UpdateInDb(List<Sensor> sensors)
        {
            await _conveyorRepo.UpdateConveyors(scene.Conveyors);
            await _palletRepo.UpdatePallets(scene.Pallets);
            await _conveyorPointRepo.UpdateConveyingPoints(scene.ConveyingPoints);
            await _sensorRepo.UpdateSensors(sensors);
        }
    }
}
