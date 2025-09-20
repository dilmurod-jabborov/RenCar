using Microsoft.EntityFrameworkCore;
using RenCar.DataAccess.Repositories;
using RenCar.Domain.Entities;
using RenCar.Service.Exeptions;
using RenCar.Service.Services.RentCompanies.Models;

namespace RenCar.Service.Services.RentCompanies;

public class RentCompanyService : IRentCompanyService
{
    private readonly IRepository<RentCompany> rentCompanyRepository;
    public RentCompanyService()
    {
        rentCompanyRepository = new Repository<RentCompany>();
    }
    public async Task CreateAsync(RentCompanyCreateModel model)
    {
        var existCompany = rentCompanyRepository
            .SelectAllAsQueryable()
            .Any(c => c.Phone == model.Phone);

        if(existCompany)
            throw new AlreadyExistException("This company already exist!");

        var createCompany = await rentCompanyRepository.InsertAsync(new RentCompany
        {
            Name = model.Name,
            Description = model.Description,
            Phone = model.Phone,
        });
    }

    public async Task UpdateAsync(int id, RentCompanyUpdateModel model)
    {
        var existCompany = await rentCompanyRepository.SelectAsync(id)
            ?? throw new NotFoundException("Company not found!");

        existCompany.Name = model.Name;
        existCompany.Description = model.Description;
        existCompany.Phone = model.Phone;

        await rentCompanyRepository.UpdateAsync(existCompany);
    }

    public async Task DeleteAsync(int id)
    {
        var existCompany = await rentCompanyRepository.SelectAsync(id)
            ?? throw new NotFoundException("Company not found!");

        await rentCompanyRepository.DeleteAsync(existCompany);
    }

    public async Task<List<RentCompanyViewModel>> GetAllAsync(string? search)
    {
        var companies = rentCompanyRepository
            .SelectAllAsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            companies = companies.Where(c =>
                c.Name.ToLower().Contains(search));
        }

        return await companies
            .Select(c => new RentCompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Phone = c.Phone
            }).ToListAsync();
    }

    public async Task<RentCompanyViewModel> GetByIdAsync(int id)
    {
        var existCompany = await rentCompanyRepository.SelectAsync(id);

        if (existCompany == null)
            throw new NotFoundException("Company not found!");

        return new RentCompanyViewModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
            Description = existCompany.Description,
            Phone = existCompany.Phone
        };
    }
}
