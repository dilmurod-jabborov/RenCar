using Microsoft.EntityFrameworkCore;
using RenCar.DataAccess.Repositories;
using RenCar.Domain.Entities;
using RenCar.Service.Exeptions;
using RenCar.Service.Services.Users.Models;

namespace RenCar.Service.Services.Users;

partial class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    public UserService()
    {
        userRepository = new Repository<User>();
    }
    public async Task CreateAsync(UserCreateModel model)
    {
        var existUser = userRepository
            .SelectAllAsQueryable()
            .Any(u => u.Phone == model.Phone);

        if (!existUser)
            throw new AlreadyExistException("This is user already exist!");

        var createUser = await userRepository.InsertAsync(new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Age = model.Age,
            Email = model.Email,
            Phone = model.Phone,
            PassportNumber = model.PassportNumber,
            Gender = model.Gender,
        });
    }

    public async Task UpdateAsync(int id, UserUpdateModel model)
    {
        var existUser = await userRepository.SelectAsync(id)
            ?? throw new NotFoundException("This user is not found!");

        existUser.FirstName = model.FirstName;
        existUser.LastName = model.LastName;
        existUser.Age = model.Age;
        existUser.Email = model.Email;
        existUser.Gender = model.Gender;
        existUser.PassportNumber = model.PassportNumber;

        await userRepository.UpdateAsync(existUser);
    }

    public async Task DeleteAsync(int id)
    {
        var existUser = await userRepository.SelectAsync(id)
            ?? throw new NotFoundException("This User is not found!");

        await userRepository.DeleteAsync(existUser);
    }

    public async Task<List<UserViewModel>> GetAllAsync(string? search)
    {
        var users = userRepository.SelectAllAsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            users = users.Where(u =>
            u.Email.ToLower().Contains(search) ||
            u.PassportNumber.ToLower().Contains(search) ||
            u.FirstName.ToLower().Contains(search) ||
            u.LastName.ToLower().Contains(search));
        }

        return await users
            .Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Age = u.Age,
                Email = u.Email,
                Phone = u.Phone,
                PassportNumber = u.PassportNumber,
            }).ToListAsync();
    }

    public async Task<UserViewModel> GetByIdAsync(int id)
    {
        var user = await userRepository.SelectAsync(id)
            ?? throw new NotFoundException("This user is not found!");

        return new UserViewModel
        {
            Id = id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age,
            Email = user.Email,
            Phone = user.Phone,
            PassportNumber = user.PassportNumber,
        };
    }
}