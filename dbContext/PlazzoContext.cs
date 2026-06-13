using Microsoft.EntityFrameworkCore;
using plazzo_api.entity;

public class PlazzoContext : DbContext
{
    // Constructeur pour le DI (Program.cs)
    public PlazzoContext(DbContextOptions<PlazzoContext> options) : base(options) { }

    public DbSet<Agency> Agencies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Goods { get; set; }
}