using RenCar.DataAccess.Repositories;
using RenCar.Domain.Entities;
using RenCar.Domain.Enums;
using RenCar.Service.Exeptions;
using RenCar.Service.Services.Bookings.Models;

namespace RenCar.Service.Services.Bookings;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> bookingRepository;
    private readonly IRepository<Car> carRepository;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Payment> paymentRepository;

    public BookingService()
    {
        bookingRepository = new Repository<Booking>();
        carRepository = new Repository<Car>();
        userRepository = new Repository<User>();
        paymentRepository = new Repository<Payment>();
    }

    public async Task CreateAsync(BookingCreateModel model)
    {
        var existCar = await carRepository.SelectAsync(model.CarId)
            ?? throw new NotFoundException("This car is not found!");

        var existUser = await userRepository.SelectAsync(model.UserId)
            ?? throw new NotFoundException($"{model.UserId} not found");

        var alreadyExistBook = bookingRepository
            .SelectAllAsQueryable()
            .Any(b => b.UserId == model.UserId && b.CarId == model.CarId);

        if (alreadyExistBook)
            throw new AlreadyExistException("You have reserved this car!");

        var totalDay = (model.EndDateTime - model.StartDateTime).Days;

        var totalPrice = totalDay * existCar.PricePerDay + existCar.PledgePrice;

        if (model.PaymentMethod == PaymentMethod.card)
        {
            var book = new Booking
            {
                UserId = model.UserId,
                CarId = model.CarId,
                TotalPrice = totalPrice,
            };

            await bookingRepository.InsertAsync(book);

            var payment = new Payment
            {
                BookingId = book.Id,
                Method = PaymentMethod.card,
                Status = Status.pending
            };

            await paymentRepository.InsertAsync(payment);
        }
        else if (model.PaymentMethod == PaymentMethod.cash)
        {
            var book = new Booking
            {
                UserId = model.UserId,
                CarId = model.CarId,
                TotalPrice = totalPrice,
            };

            await bookingRepository.InsertAsync(book);

            var payment = new Payment
            {
                BookingId = book.Id,
                Method = PaymentMethod.cash,
                Status = Status.pending
            };

            await paymentRepository.InsertAsync(payment);
        }
        else if (model.PaymentMethod == PaymentMethod.online)
        {
            var payment = await PaymentsAsync(totalPrice, model);

            if (!payment)
                throw new PaymentFailedException("You have not deposited enough money!");

            var book = new Booking
            {
                UserId = model.UserId,
                CarId = model.CarId,
                TotalPrice = totalPrice,
            };

            await bookingRepository.InsertAsync(book);

            var createPay = new Payment
            {
                BookingId = book.Id,
                Method = PaymentMethod.online,
                Status = Status.confirmed,
                PaymentDate = DateTime.UtcNow,
            };

            await paymentRepository.InsertAsync(createPay);
        }
    }

    public async Task UpdateAsync(int id, BookingUpdateModel model)
    {
        var existBook = await bookingRepository.SelectAsync(id)
            ?? throw new NotFoundException("This book is not found!");

        var existCar = await carRepository.SelectAsync(existBook.CarId)
            ?? throw new NotFoundException("This car is not found!");

        var totalDay = (model.EndDateTime - model.StartDateTime).Days;

        var totalPrice = totalDay * existCar.PricePerDay + existCar.PledgePrice;

        existBook.BookDetails.DropUpLocationId = model.DropUpLocationId;
        existBook.BookDetails.PickUpLocationId = model.PickUpLocationId;
        existBook.BookDetails.StartDateTime = model.StartDateTime;
        existBook.BookDetails.EndDateTime = model.EndDateTime;

        await bookingRepository.UpdateAsync(existBook);
    }

    public async Task CancelBookingAsync(int userId, int bookId)
    {
        var existBook = await bookingRepository.SelectAsync(bookId)
            ?? throw new NotFoundException("This book is not found!");

        var userExist = await userRepository.SelectAsync(userId)
            ?? throw new NotFoundException("This user is not found!");

        await bookingRepository.DeleteAsync(existBook);
    }

    public Task<List<BookingViewModel>> GetAllAsync(string? search)
    {
        throw new NotFiniteNumberException();
    }

    public Task<BookingViewModel> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PaymentsAsync(decimal totalPrice, BookingCreateModel model)
    {
        if (model.Amount == totalPrice)
            return true;

        else
            return false;
    }
}
