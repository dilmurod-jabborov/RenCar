using RenCar.Service.Services.RentCompanies.Models;

namespace RenCar.Service.Services.RentCompanies;

public interface IRentCompanyService
{
    Task CreateAsync(RentCompanyCreateModel model);
    Task UpdateAsync(int id, RentCompanyUpdateModel model);
    Task DeleteAsync(int id);
    Task<List<RentCompanyViewModel>> GetAllAsync(string? search);
    Task<RentCompanyViewModel> GetByIdAsync(int id);
}
