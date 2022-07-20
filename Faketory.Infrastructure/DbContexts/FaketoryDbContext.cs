using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using Faketory.Domain.Resources.PLCRelated;
using Microsoft.EntityFrameworkCore;

namespace Faketory.Infrastructure.DbContexts
{
    public class FaketoryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PlcModel> PlcModels { get; set; }
        public DbSet<PlcEntity> Plcs { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<IO> InputsOutputs { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<Conveyor> Conveyors { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Machine> Machines { get; set; }

        public FaketoryDbContext(DbContextOptions<FaketoryDbContext> options) : base(options)
        {
                
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FaketoryDbContext).Assembly);
        }

    }
}
