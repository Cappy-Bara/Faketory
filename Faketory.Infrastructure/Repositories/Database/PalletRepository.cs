﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.IRepositories;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.Repositories.Database
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

        public async Task<List<Pallet>> GetAllUserPallets()
        {
            return await _dbContext.Pallets.ToListAsync();
        }

        public async Task<Pallet> GetPallet(Guid palletId)
        {
            return await _dbContext.Pallets.FirstOrDefaultAsync(x => x.Id == palletId);
        }

        public async Task<bool> PalletCollides(int posX, int posY)
        {
            return await _dbContext.Pallets.AnyAsync(x => x.PosX == posX && x.PosY == posY);
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