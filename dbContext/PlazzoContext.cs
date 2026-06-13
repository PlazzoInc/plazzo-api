using Microsoft.EntityFrameworkCore;
using plazzo_api.entity;

namespace plazzo_api.dbContext;
public class PlazzoContext : DbContext
{
    // Constructeur pour le DI (Program.cs)
    public PlazzoContext(DbContextOptions<PlazzoContext> options) : base(options) { }

    public DbSet<Agency> Agencies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Goods { get; set; }

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
    }
}