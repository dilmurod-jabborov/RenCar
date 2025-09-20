using Microsoft.EntityFrameworkCore;
using RenCar.DataAccess.Repositories;
using RenCar.Domain.Entities;
using RenCar.Service.Exeptions;
using RenCar.Service.Services.Locations.Models;

namespace RenCar.Service.Services.Locations;

public class LocationService : ILocationService
{
    private readonly IRepository<Location> locationRepository;
    public LocationService()
    {
        locationRepository = new Repository<Location>();
    }
    public async Task CreateAsync(LocationCreateModel model)
    {
        var exist = await locationRepository
            .SelectAllAsQueryable()
            .AnyAsync(l => l.Address == model.Address);

        if (exist)
            throw new AlreadyExistException("This location already exist!");

        var createLocation = await locationRepository.InsertAsync(new Location
        {
            City = model.City,
            Address = model.Address
        });
    }

    public async Task UpdateAsync(int id, LocationUpdateModel model)
    {
        var existLocation = await locationRepository.SelectAsync(id)
            ?? throw new NotFoundException("Location not found!");

        existLocation.City = model.City;
        existLocation.Address = model.Address;

        await locationRepository.UpdateAsync(existLocation);
    }

    public async Task DeleteAsync(int id)
    {
        var existLocation = await locationRepository.SelectAsync(id)
            ?? throw new NotFoundException("Location not found!");

        await locationRepository.DeleteAsync(existLocation);
    }

    public async Task<List<LocationViewModel>> GetAllAsync(string? search)
    {
        var locations = locationRepository
            .SelectAllAsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            locations = locations.Where(l => 
                l.City.ToLower().Contains(search) ||
                l.Address.ToLower().Contains(search));
        }

        return await locations
            .Select(l => new LocationViewModel
            {
                Id = l.Id,
                City = l.City,
                Address = l.Address
            })
            .ToListAsync();
    }

    public async Task<LocationViewModel> GetAsync(int id)
    {
        var existLocation = await locationRepository.SelectAsync(id)
            ?? throw new NotFoundException("Location not found!");

        return new LocationViewModel
        {
            Id = existLocation.Id,
            City = existLocation.City,
            Address = existLocation.Address
        };
    }
}
