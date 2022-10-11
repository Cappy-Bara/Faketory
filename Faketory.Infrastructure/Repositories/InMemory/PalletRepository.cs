using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;

namespace Faketory.Infrastructure.Repositories.InMemory
{
    public class PalletRepository : IPalletRepository
    {
        private readonly FaketoryInMemoryDbContext context;

        public PalletRepository(FaketoryInMemoryDbContext dbContext)
        {
            context = dbContext;
        }

        public async Task AddPallet(Pallet pallet)
        {
            pallet.Id = Guid.NewGuid();

            context.Pallets.Add(pallet.Id,pallet);
            await context.Persist();
        }

        public Task<List<Pallet>> GetAllUserPallets()
        {
            return Task.FromResult(context.Pallets.Values.ToList());
        }

        public Task<Pallet> GetPallet(Guid palletId)
        {
            context.Pallets.TryGetValue(palletId, out var output);
            return Task.FromResult(output);
        }

        public Task<bool> PalletCollides(int posX, int posY)
        {
            return Task.FromResult(context.Pallets.Values.Any(x => x.PosX == posX && x.PosY == posY));
        }

        public async Task RemovePallet(Pallet pallet)
        {
            context.Pallets.Remove(pallet.Id);
            await context.Persist();
        }

        public async Task UpdatePallet(Pallet pallet)
        {
            context.Pallets[pallet.Id] = pallet;
            await context.Persist();
        }

        public async Task UpdatePallets(List<Pallet> pallets)
        {
            foreach (var pallet in pallets)
            {
                context.Pallets[pallet.Id] = pallet;
            }

            await context.Persist();
        }
    }
}
