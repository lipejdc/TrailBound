using Microsoft.EntityFrameworkCore;
using TrailBound.Domain.Entities;

namespace TrailBound.Infrastructure.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Tells EF Core that Location is a value object owned by Activity
        modelBuilder.Entity<Activity>()
            .OwnsOne(a => a.Location);
    }
}
