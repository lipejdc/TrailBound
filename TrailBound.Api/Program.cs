using Scalar.AspNetCore;
using TrailBound.Application.Interfaces;
using TrailBound.Application.Services;
using TrailBound.Infrastructure.Persistence.DatabaseContext;
using TrailBound.Infrastructure.Persistence.Repositories;
using TrailBound.Infrastructure.Repositories;
using TrailBound.KomootWrapper;
using TrailBound.KomootWrapper.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient<KomootClient>(client =>
{
    client.BaseAddress = new Uri("https://www.komoot.com");
});

builder.Services.AddControllers();
builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IKomootClient, KomootClient>();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();
