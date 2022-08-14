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
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder.ToTable("Sensor");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PosX).IsRequired();
            builder.Property(x => x.PosY).IsRequired();
            builder.Property(x => x.IOId).IsRequired();

            builder.HasOne(x => x.IO).WithMany().HasForeignKey(x => x.IOId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
