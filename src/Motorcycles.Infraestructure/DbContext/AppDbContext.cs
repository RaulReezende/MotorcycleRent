using Microsoft.EntityFrameworkCore;
using Motorcycles.Domain.Entities;
using Motorcycles.Infraestructure.EntityConfiguration;

namespace Motorcycles.Infraestructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public AppDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new DeliveryManConfiguration());
        builder.ApplyConfiguration(new MotorcycleConfiguration());
        builder.ApplyConfiguration(new RentalConfiguration());
        builder.ApplyConfiguration(new MotorcycleRegisteredEventConfiguration());
    }

    public DbSet<DeliveryMan> DeliveryMens { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<MotorcycleRegisteredEvent> MotorcycleEvents { get; set; }
}
