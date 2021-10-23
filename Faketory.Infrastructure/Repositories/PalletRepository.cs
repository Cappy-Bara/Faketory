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
    public class PalletRepository : IPalletRepository
    {
        private readonly FaketoryDbContext _dbContext;
        public PalletRepository(FaketoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPallet(Pallet pallet)
        {
            await _dbContext.Pallets.AddAsync(pallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Pallet>> GetAllUserPallets(string email)
        {
            return await _dbContext.Pallets.Where(x => x.UserEmail == email).ToListAsync();
        }

        //TODO - PO CO JEST USEREMAIL
        public async Task<Pallet> GetPallet(Guid palletId, string userEmail)
        {
            return await _dbContext.Pallets.FirstOrDefaultAsync(x => x.Id == palletId && x.UserEmail == userEmail);
        }

        public async Task<bool> PalletCollides(int posX, int posY, string email)
        {
           return await _dbContext.Pallets.AnyAsync(x => x.PosX == posX && x.PosY == posY && x.UserEmail == email);
        }

        public async Task RemovePallet(Pallet pallet)
        {
            _dbContext.Pallets.Remove(pallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePallet(Pallet pallet)
        {
            _dbContext.Update(pallet);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePallets(List<Pallet> pallets)
        {
            _dbContext.UpdateRange(pallets);
            await _dbContext.SaveChangesAsync();
        }
    }
}
