using Faketory.Domain.Resources.IndustrialParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faketory.Infrastructure.DbConfigurations
{
    public class MachinesConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.ToTable("Machines");

            builder.HasKey(x => x.Id);
        }
    }
}
