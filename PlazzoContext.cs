using Microsoft.EntityFrameworkCore;

public class PlazzoContext : DbContext
{
    // Constructeur pour le DI (Program.cs)
    public PlazzoContext(DbContextOptions<PlazzoContext> options) : base(options) { }

    public DbSet<Agencies> Agencies { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Goods> Goods { get; set; }
}