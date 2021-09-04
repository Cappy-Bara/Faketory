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
    public class ConveyorConfiguration : IEntityTypeConfiguration<Conveyor>
    {
        public void Configure(EntityTypeBuilder<Conveyor> builder)
        {
            builder.ToTable("Conveyors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserEmail).IsRequired();
            builder.Property(x => x.IOId).IsRequired();
            builder.Property(x => x.Frequency).IsRequired();
            builder.Property(x => x.IsTurnedDownOrLeft).IsRequired();
            builder.Property(x => x.IsVertical).IsRequired();
            builder.Property(x => x.Length).IsRequired();
            builder.Property(x => x.PosX).IsRequired();
            builder.Property(x => x.PosY).IsRequired();

            builder.HasOne(x => x.IO).WithMany().HasForeignKey(x => x.IOId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.ConveyingPoints).WithOne(x => x.Conveyor)
                .HasForeignKey(x => x.ConveyorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
