using Scalar.AspNetCore;
using TrailBound.Application.Interfaces;
using TrailBound.Infrastructure;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();
