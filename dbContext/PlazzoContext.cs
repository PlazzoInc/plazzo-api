using Microsoft.EntityFrameworkCore;
using plazzo_api.entity;

namespace plazzo_api.dbContext;
public class PlazzoContext : DbContext
{
    public PlazzoContext(DbContextOptions<PlazzoContext> options) : base(options) { }

    public DbSet<Agency> Agencies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Goods { get; set; }
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<PropertyAddress> PropertyAddresses => Set<PropertyAddress>();
    public DbSet<PropertyPhoto> PropertyPhotos => Set<PropertyPhoto>();
    public DbSet<PropertyFeature> PropertyFeatures => Set<PropertyFeature>();
    public DbSet<Mandate> Mandates => Set<Mandate>();
    public DbSet<Offer> Offers => Set<Offer>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Agency)
            .WithMany(a => a.Users)
            .HasForeignKey(u => u.AgencyId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Property>()
            .Property(p => p.Type)
            .HasConversion<string>();

         modelBuilder.Entity<Property>()
           .Property(p => p.Status)
           .HasConversion<string>();

         modelBuilder.Entity<Property>()
            .Property(p => p.Price)
            .HasPrecision(12, 2);

        modelBuilder.Entity<Property>()
            .Property(p => p.SurfaceArea)
            .HasPrecision(8, 2);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Agency)
            .WithMany()
            .HasForeignKey(p => p.AgencyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Commercial)
            .WithMany()
            .HasForeignKey(p => p.CommercialId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PropertyAddress>()
            .HasOne(a => a.Property)
            .WithOne(p => p.Address)
            .HasForeignKey<PropertyAddress>(a => a.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PropertyAddress>()
            .Property(a => a.Latitude)
            .HasPrecision(10, 7);

        modelBuilder.Entity<PropertyAddress>()
            .Property(a => a.Longitude)
            .HasPrecision(10, 7);

        modelBuilder.Entity<PropertyAddress>()
            .HasOne(a => a.Property)
            .WithOne(p => p.Address)
            .HasForeignKey<PropertyAddress>(a => a.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<PropertyAddress>()
            .Property(a => a.Latitude)
            .HasPrecision(10, 7);

        modelBuilder.Entity<PropertyAddress>()
            .Property(a => a.Longitude)
            .HasPrecision(10, 7);

        modelBuilder.Entity<PropertyPhoto>()
            .HasOne(p => p.Property)
            .WithMany(p => p.Photos)
            .HasForeignKey(p => p.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PropertyFeature>()
            .HasOne(f => f.Property)
            .WithMany(p => p.Features)
            .HasForeignKey(f => f.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Mandate>()
            .Property(m => m.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Mandate>()
            .Property(m => m.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Mandate>()
            .Property(m => m.FeePercentage)
            .HasPrecision(4, 2);

        modelBuilder.Entity<Mandate>()
            .HasOne(m => m.Property)
            .WithMany()
            .HasForeignKey(m => m.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Mandate>()
            .HasOne(m => m.Client)
            .WithMany()
            .HasForeignKey(m => m.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Mandate>()
            .HasOne(m => m.Commercial)
            .WithMany()
            .HasForeignKey(m => m.CommercialId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Offer>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Offer>()
            .Property(o => o.Amount)
            .HasPrecision(12, 2);

        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Property)
            .WithMany()
            .HasForeignKey(o => o.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Buyer)
            .WithMany()
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Offer>()
            .HasOne(o => o.Commercial)
            .WithMany()
            .HasForeignKey(o => o.CommercialId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .Property(t => t.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Transaction>()
            .Property(t => t.FinalPrice)
            .HasPrecision(12, 2);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Property)
            .WithMany()
            .HasForeignKey(t => t.PropertyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Offer)
            .WithMany()
            .HasForeignKey(t => t.OfferId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Seller)
            .WithMany()
            .HasForeignKey(t => t.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Buyer)
            .WithMany()
            .HasForeignKey(t => t.BuyerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}