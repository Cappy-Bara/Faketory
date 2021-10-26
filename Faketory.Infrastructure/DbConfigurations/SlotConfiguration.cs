using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources.PLCRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faketory.Infrastructure.DbConfigurations
{
    public class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable("Slots");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Plc)
                .WithOne();

            builder.HasMany(x => x.InputsOutputs)
                .WithOne()
                .HasForeignKey(x => x.SlotId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Plc)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
