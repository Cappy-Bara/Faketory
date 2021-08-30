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
        private Scene scene; 

        public SceneHandler(IPalletRepository palletRepo, IConveyorRepository conveyorRepo,IConveyingPointRepository conveyorPointRepo, string email)
        {
            _palletRepo = palletRepo;
            _conveyorRepo = conveyorRepo;
            _conveyorPointRepo = conveyorPointRepo;
            scene = new Scene(email, conveyorRepo, palletRepo);
        }
        public async Task Timestamp()
        {
            await scene.CreateScene();
            
            scene.UpdateConveyorsStatus();
            await scene.BindPalletToPoint();
            scene.MarkStaticBlocksAsMoved();
            MoveBlocks();
            await UpdateInDb();
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
        public async Task UpdateInDb()
        {
            await _conveyorRepo.UpdateConveyors(scene.Conveyors);
            await _palletRepo.UpdatePallets(scene.Pallets);
            await _conveyorPointRepo.UpdateConveyingPoints(scene.ConveyingPoints);
        }


    }
}
