using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Faketory.Domain.Resources;
using Faketory.Domain.Resources.PLCRelated;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Faketory.Infrastructure.DbConfigurations
{
    public class PlcEntityConfiguration : IEntityTypeConfiguration<PlcEntity>
    {
        public void Configure(EntityTypeBuilder<PlcEntity> builder)
        {
            builder.ToTable("Plcs");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Model)
                .WithMany()
                .HasForeignKey(x => x.ModelId);
        }
    }
}
