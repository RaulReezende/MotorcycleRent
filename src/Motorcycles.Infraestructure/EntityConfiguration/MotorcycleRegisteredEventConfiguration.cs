using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycles.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Infraestructure.EntityConfiguration;

internal class MotorcycleRegisteredEventConfiguration : IEntityTypeConfiguration<MotorcycleRegisteredEvent>
{
    public void Configure(EntityTypeBuilder<MotorcycleRegisteredEvent> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.MotorcycleId).IsRequired();
        builder.Property(m => m.Plate).IsRequired();
        builder.Property(m => m.Year).IsRequired();
        builder.Property(m => m.Model).IsRequired();
        builder.Property(m => m.RegisteredAt).IsRequired();

    }
}