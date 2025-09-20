using RenCar.Domain.Enums;

namespace RenCar.Service.Services.Bookings.Models;

public class BookingViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CarId { get; set; }
    public decimal TotalPrice { get; set; }

    public class BookDetailInfo
    {
        public int Id { get; set; }
        public int PickUpLocationId { get; set; }
        public int DropUpLocationId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public Status Status { get; set; }
    }

    public class PaymentInfo
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public Status Status { get; set; }
    }
}
