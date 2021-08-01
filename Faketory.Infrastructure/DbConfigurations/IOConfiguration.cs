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
    public class IOConfiguration : IEntityTypeConfiguration<IO>
    {
        public void Configure(EntityTypeBuilder<IO> builder)
        {
            builder.ToTable("InputsOutputs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Bit).IsRequired();
            builder.Property(x => x.Byte).IsRequired();
        }
    }
}
