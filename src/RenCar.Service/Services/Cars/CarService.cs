using Microsoft.EntityFrameworkCore;
using RenCar.DataAccess.Repositories;
using RenCar.Domain.Entities;
using RenCar.Service.Exeptions;
using RenCar.Service.Services.Cars.Models;

namespace RenCar.Service.Services.Cars;

public class CarService : ICarServcie
{
    private readonly IRepository<Car> carRepository;
    private readonly IRepository<RentCompany> rentCompanyRepository;
    private readonly IRepository<CarDetails> carDetailsRepository;
    private readonly IRepository<CarLocation> carLocationRepository;
    public CarService()
    {
        carLocationRepository = new Repository<CarLocation>();
        carDetailsRepository = new Repository<CarDetails>();
        carRepository = new Repository<Car>();
        rentCompanyRepository = new Repository<RentCompany>();
    }
    public async Task CreateAsync(CarCreateModel model)
    {
        _ = rentCompanyRepository.SelectAsync(model.CompanyId)
            ?? throw new NotFoundException("This company is not found!");

        var carCreate = new Car
        {
            CompanyId = model.CompanyId,
            Brand = model.Brand,
            Model = model.Model,
            PricePerDay = model.PricePerDay,
            PledgePrice = model.PledgePrice,
            PickUpLocationId = model.PickUpLocationId,
            Available = true
        };

        await carRepository.InsertAsync(carCreate);

        var carDetails = new CarDetails
        {
            CarId = carCreate.Id,
            Year = model.Year,
            Seats = model.Seats,
            FuelType = model.FuelType,
            Transmission = model.Transmission
        };

        await carDetailsRepository.InsertAsync(carDetails);

        if (model.DropOffLocationId != null && model.DropOffLocationId.Any())
        {
            foreach (var locationId in model.DropOffLocationId)
            {
                var carLocation = new CarLocation
                {
                    CarId = carCreate.Id,
                    LocationId = locationId
                };
                await carLocationRepository.InsertAsync(carLocation);
            }
        }
        else
        {
            var carLocation = new CarLocation
            {
                CarId = carCreate.Id,
                LocationId = carCreate.PickUpLocationId
            };

            await carLocationRepository.InsertAsync(carLocation);
        }
    }

    public async Task UpdateAsync(int id, CarUpdateModel model)
    {
        _ = rentCompanyRepository.SelectAsync(model.CompanyId)
            ?? throw new NotFoundException("This is company not found!");

        var existCar = await carRepository.SelectAsync(id)
            ?? throw new NotFoundException("This is car not found!");

        existCar.Brand = model.Brand;
        existCar.Model = model.Model;
        existCar.PricePerDay = model.PricePerDay;
        existCar.PledgePrice = model.PledgePrice;
        existCar.CarDetails.Year = model.Year;
        existCar.CarDetails.Seats = model.Seats;
        existCar.CarDetails.Transmission = model.Transmission;
        existCar.CarDetails.FuelType = model.FuelType;

        await carRepository.UpdateAsync(existCar);
    }

    public async Task DeleteAsync(int id)
    {
        var existCar = await carRepository.SelectAsync(id)
            ?? throw new NotFoundException("This car is not found!");

        await carRepository.DeleteAsync(existCar);
    }

    public async Task<List<CarViewModel>> GetAllAsync(string? search)
    {
        var cars = carRepository.SelectAllAsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            cars = cars.Where(c =>
            c.Brand.ToLower().Contains(search) ||
            c.Model.ToLower().Contains(search));
        }

        return await cars.Select(c => new CarViewModel
        {
            Id = c.Id,
            CompanyId = c.CompanyId,
            Brand = c.Brand,
            Model = c.Model,
            PricePerDay = c.PricePerDay,
            PledgePrice = c.PledgePrice,
            Available = c.Available,
            CarDetailInfos = new CarViewModel.CarDetailInfo
            {
                Id = c.CarDetails.Id,
                Year = c.CarDetails.Year,
                Seats = c.CarDetails.Seats,
                Transmission = c.CarDetails.Transmission,
                FuelType = c.CarDetails.FuelType,
            }
        }).ToListAsync();
    }

    public async Task<CarViewModel> GetAsync(int id)
    {
        _ = await rentCompanyRepository.SelectAsync(id)
            ?? throw new NotFoundException("This company is not found!");

        var existCar = await carRepository.SelectAsync(id)
            ?? throw new NotFoundException("This car is not found!");

        var car = new CarViewModel
        {
            Id = id,
            CompanyId = existCar.CompanyId,
            Brand = existCar.Brand,
            Model = existCar.Model,
            PricePerDay = existCar.PricePerDay,
            PledgePrice = existCar.PledgePrice,
            CarDetailInfos = new CarViewModel.CarDetailInfo
            {
                Id = existCar.CarDetails.Id,
                Year = existCar.CarDetails.Year,
                Seats = existCar.CarDetails.Seats,
                FuelType = existCar.CarDetails.FuelType,
                Transmission = existCar.CarDetails.Transmission,
            }
        };

        return car;
    }
}
