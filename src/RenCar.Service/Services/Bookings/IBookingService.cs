namespace RenCar.Service.Services.Bookings;

public interface IBookingService
{
    Task CreateAsync();
    Task UpdateAsync();
    Task DeleteAsync();
    Task GetAsync();
    Task GetAllAsync();
}