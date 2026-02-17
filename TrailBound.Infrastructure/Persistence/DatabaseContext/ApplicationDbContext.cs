using Microsoft.EntityFrameworkCore;
using TrailBound.Domain.Entities;

namespace TrailBound.Infrastructure.Persistence.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; set; }
    public DbSet<Trip> Trips { get; set; }
}
