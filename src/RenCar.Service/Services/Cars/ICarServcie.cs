using RenCar.Service.Services.Cars.Models;

namespace RenCar.Service.Services.Cars;

public interface ICarServcie
{
    Task CreateAsync(CarCreateModel model);
    Task UpdateAsync(int id, CarUpdateModel model);
    Task DeleteAsync(int id);
    Task<CarViewModel> GetAsync(int id);
    Task<List<CarViewModel>> GetAllAsync(string search);
}
