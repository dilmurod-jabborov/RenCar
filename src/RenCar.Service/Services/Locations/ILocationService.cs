using RenCar.Service.Services.Locations.Models;

namespace RenCar.Service.Services.Locations;

public interface ILocationService
{
    Task CreateAsync(LocationCreateModel model);
    Task UpdateAsync(int id, LocationUpdateModel model);
    Task DeleteAsync(int id);
    Task<LocationViewModel> GetAsync(int id);
    Task<List<LocationViewModel>> GetAllAsync(string search);
}
