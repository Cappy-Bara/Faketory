using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;

namespace Faketory.Domain.Aggregates
{
    public class Scene
    {
        public List<Conveyor> Conveyors { get; set; } = new List<Conveyor>();
        public List<ConveyingPoint> ConveyingPoints { get; set; } = new List<ConveyingPoint>();
        public List<Pallet> Pallets { get; set; } = new List<Pallet>();
        private readonly string userEmail;
        private readonly IConveyorRepository _conveyorRepo;
        private readonly IPalletRepository _palletRepo;

        public  Scene(string email, IConveyorRepository conveyorRepo, IPalletRepository palletRepo)
        {
            userEmail = email;
            _conveyorRepo = conveyorRepo;
            _palletRepo = palletRepo;
        }
        public async Task CreateScene()
        {
            Conveyors.AddRange(await _conveyorRepo.GetAllUserConveyors(userEmail));
            ConveyingPoints.AddRange(Conveyors.SelectMany(x => x.ConveyingPoints));
            Pallets.AddRange((await _palletRepo.GetAllUserPallets(userEmail)));
        }
        public void MarkStaticBlocksAsMoved()
        {
            foreach (Pallet block in Pallets)
                if (ConveyingPoints.FirstOrDefault(x => x.PosX == block.PosX && x.PosY == block.PosY) == null ||
                    !ConveyingPoints.FirstOrDefault(x => x.PosX == block.PosX && x.PosY == block.PosY).Conveyor.IsRunning)
                    block.MovementFinished = true;
        }
        public async Task BindPalletToPoint()
        {
            foreach (Pallet p in Pallets)
            {
                var cp = ConveyingPoints.FirstOrDefault(x => x.PosX == p.PosX && x.PosY == p.PosY);
                if (cp != null)
                    cp.PalletToMove = p;
            }
        }
        public bool NoObstacles(int x, int y)
        {
            var field = Pallets.FirstOrDefault(k => k.PosX == x && k.PosY == y);
            if (field == null)
                return true;
            return false;
        }
        public bool StaticObstacle(int x, int y)
        {
            var field = Pallets.FirstOrDefault(k => k.PosX == x && k.PosY == y);
            if (field.MovementFinished)
                return true;
            return false;
        }
        public void UpdateConveyorsStatus()
        {
            Conveyors.ForEach(x => x.RefreshConveyorStatus());
        }
    }
}
