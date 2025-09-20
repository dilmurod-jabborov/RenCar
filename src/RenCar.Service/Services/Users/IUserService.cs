using RenCar.Service.Services.Users.Models;

namespace RenCar.Service.Services.Users;

partial interface IUserService
{
    Task CreateAsync(UserCreateModel model);
    Task UpdateAsync(int id, UserUpdateModel model);
    Task DeleteAsync(int id);
    Task<List<UserViewModel>> GetAllAsync(string? search);
    Task<UserViewModel> GetByIdAsync(int id);
}
