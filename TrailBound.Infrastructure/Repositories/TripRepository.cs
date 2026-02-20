using Microsoft.EntityFrameworkCore;
using TrailBound.Application.Dtos.Trip;
using TrailBound.Application.Interfaces;
using TrailBound.Application.Mappers;
using TrailBound.Domain.Entities;
using TrailBound.Infrastructure.Persistence.DatabaseContext;

namespace TrailBound.Infrastructure.Repositories;

public class TripRepository(ApplicationDbContext context) : ITripRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<ReadTripDto> CreateTripAsync(CreateTripDto createTripDto)
    {
        var trip = new Trip
        {
            Title = createTripDto.Title,
            StartDate = createTripDto.StartDate,
            EndDate = createTripDto.EndDate,
            Categories = createTripDto.Categories,
            Location = new Location
            {
                Country = createTripDto.Country,
                City = createTripDto.City,
                Area = createTripDto.Area
            },
            GoogleMapsUrl = createTripDto.GoogleMapsUrl
        };

        _context.Trips.Add(trip);

        await _context.SaveChangesAsync();

        var resultDto = new ReadTripDto
        {
            Id = trip.Id,
            Title = trip.Title,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Categories = trip.Categories,
            Country = trip.Location.Country,
            City = trip.Location.City,
            Area = trip.Location.Area,
            GoogleMapsUrl = trip.GoogleMapsUrl
        };

        return resultDto;
    }

    public async Task<bool> DeleteTripAsync(int id)
    {
        var trip = await _context.Trips.FindAsync(id);

        if (trip == null)
        {
            return false;
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ReadTripDto?> GetTripByIdAsync(int id)
    {
        var trip = await _context.Trips.FindAsync(id);

        if (trip == null)
        {
            return null;
        }

        var resultDto = new ReadTripDto
        {
            Id = trip.Id,
            Title = trip.Title,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Categories = trip.Categories,
            Country = trip.Location.Country,
            City = trip.Location.City,
            Area = trip.Location.Area,
            GoogleMapsUrl = trip.GoogleMapsUrl
        };

        return resultDto;
    }

    public async Task<List<ReadTripDto>> GetTripsAsync()
    {
        var trips = await _context.Trips
            .Select(t => new ReadTripDto
            {
                Id = t.Id,
                Title = t.Title,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Categories = t.Categories,
                Country = t.Location.Country,
                City = t.Location.City,
                Area = t.Location.Area,
                GoogleMapsUrl = t.GoogleMapsUrl
            })
            .ToListAsync();

        return trips;
    }

    public async Task<List<ReadTripDto>> GetTripsByYearAsync(int year)
    {
        var trips = await _context.Trips
        .Where(t => t.StartDate.Year == year)
        .Select(t => new ReadTripDto
        {
            Id = t.Id,
            Title = t.Title,
            StartDate = t.StartDate,
            EndDate = t.EndDate,
            Categories = t.Categories,
            Country = t.Location.Country,
            City = t.Location.City,
            Area = t.Location.Area,
            GoogleMapsUrl = t.GoogleMapsUrl
        })
        .ToListAsync();

        return trips;
    }

    public async Task<ReadTripDto?> UpdateTripAsync(int id, UpdateTripDto updateTripDto)
    {
        var trip = await _context.Trips.FindAsync(id);

        if (trip == null)
        {
            return null;
        }

        trip.Title = updateTripDto.Title ?? trip.Title;
        trip.StartDate = updateTripDto.StartDate ?? trip.StartDate;
        trip.EndDate = updateTripDto.EndDate ?? trip.EndDate;
        trip.Categories = updateTripDto.Categories ?? trip.Categories;
        trip.Location.Country = updateTripDto.Country ?? trip.Location.Country;
        trip.Location.City = updateTripDto.City ?? trip.Location.City;
        trip.Location.Area = updateTripDto.Area ?? trip.Location.Area;
        trip.GoogleMapsUrl = updateTripDto.GoogleMapsUrl ?? trip.GoogleMapsUrl;

        var resultDto = new ReadTripDto
        {
            Id = trip.Id,
            Title = trip.Title,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Categories = trip.Categories,
            Country = trip.Location.Country,
            City = trip.Location.City,
            Area = trip.Location.Area,
            GoogleMapsUrl = trip.GoogleMapsUrl,
        };

        return resultDto;
    }
}
