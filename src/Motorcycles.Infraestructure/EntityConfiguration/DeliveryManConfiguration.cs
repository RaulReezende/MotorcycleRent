using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycles.Domain.Entities;


namespace Motorcycles.Infraestructure.EntityConfiguration;

internal class DeliveryManConfiguration : IEntityTypeConfiguration<DeliveryMan>
{
    public void Configure(EntityTypeBuilder<DeliveryMan> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Identifier).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Name).HasMaxLength(50).IsRequired();
        builder.Property(m => m.CNPJ).HasMaxLength(14).IsRequired();
        builder.Property(m => m.Birth_Date).HasColumnType("date").IsRequired();
        builder.Property(m => m.CNH_Number).HasMaxLength(100).IsRequired();
        builder.Property(m => m.CNH_Type).HasColumnType("varchar(20)").IsRequired();
        builder.Property(m => m.CNH_Image).IsRequired(false);

    }
}
