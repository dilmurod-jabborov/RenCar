using System.ComponentModel.DataAnnotations.Schema;
using RenCar.Domain.Enums;

namespace RenCar.Service.Services.Bookings.Models;

public class BookingCreateModel
{
    public int UserId { get; set; }
    public int CarId { get; set; }

    public int PickUpLocationId { get; set; }
    public int DropUpLocationId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod Method { get; set; }
}
