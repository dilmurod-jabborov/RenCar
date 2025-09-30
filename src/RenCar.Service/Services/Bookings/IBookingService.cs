using RenCar.Service.Services.Bookings.Models;

namespace RenCar.Service.Services.Bookings;

public interface IBookingService
{
    Task CreateAsync(BookingCreateModel model);
    Task<bool> PaymentsAsync(decimal totalPrice, BookingCreateModel model);
    Task UpdateAsync(int id, BookingUpdateModel model);
    Task CancelBookingAsync(int userId, int bookId);
    Task<BookingViewModel> GetAsync(int id);
    Task<List<BookingViewModel>> GetAllAsync(string? search);
}