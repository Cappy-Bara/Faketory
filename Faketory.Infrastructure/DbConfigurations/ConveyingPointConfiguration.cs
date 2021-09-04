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
    public class ConveyingPointConfiguration : IEntityTypeConfiguration<ConveyingPoint>
    {
        public void Configure(EntityTypeBuilder<ConveyingPoint> builder)
        {
            builder.ToTable("ConveyingPoints");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConveyorId).IsRequired();
            builder.Property(x => x.PosX).IsRequired();
            builder.Property(x => x.PosY).IsRequired();
            builder.Property(x => x.Delay).IsRequired();
            builder.Property(x => x.LastPoint).IsRequired();

            builder.HasOne(x => x.Conveyor).WithMany()
                .HasForeignKey(x => x.ConveyorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
