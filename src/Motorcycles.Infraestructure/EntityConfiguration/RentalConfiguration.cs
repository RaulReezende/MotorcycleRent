using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycles.Domain.Entities;

namespace Motorcycles.Infraestructure.EntityConfiguration;

internal class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(r => r.DeliveryMan)
            .WithMany()
            .HasForeignKey(r => r.DeliveryManId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Motorcycle)
            .WithMany()
            .HasForeignKey(r => r.MotorcycleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);


        builder.Property(m => m.DeliveryManId).IsRequired();
        builder.Property(m => m.MotorcycleId).IsRequired();
        builder.Property(m => m.InitDate).HasColumnType("date").IsRequired();
        builder.Property(m => m.EndDate).HasColumnType("date").IsRequired();
        builder.Property(m => m.PrevEndDate).HasColumnType("date").IsRequired();
        builder.Property(m => m.DevolutionDate).HasColumnType("date").IsRequired(false);
        builder.Property(m => m.Plan).IsRequired();


    }
}
