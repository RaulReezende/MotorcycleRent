using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycles.Domain.Entities;

namespace Motorcycles.Infraestructure.EntityConfiguration;

internal class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Identifier).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Year).IsRequired();
        builder.Property(m => m.Model).HasMaxLength(20).IsRequired();
        builder.Property(m => m.Plate).HasMaxLength(20).IsRequired();

    }
}
