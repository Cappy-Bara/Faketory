using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.IndustrialParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faketory.Infrastructure.DbConfigurations
{
    public class PalletConfiguration : IEntityTypeConfiguration<Pallet>
    {
        public void Configure(EntityTypeBuilder<Pallet> builder)
        {
            builder.ToTable("Pallets");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PosX).IsRequired();
            builder.Property(x => x.PosY).IsRequired();
            builder.Property(x => x.UserEmail).IsRequired();

        }
    }
}
