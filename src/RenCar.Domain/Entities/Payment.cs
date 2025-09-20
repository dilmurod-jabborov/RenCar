using System.ComponentModel.DataAnnotations.Schema;
using RenCar.Domain.Enums;

namespace RenCar.Domain.Entities;

public class Payment : AudiTable
{
    public int BookingId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentMethod Method { get ; set; }
    public Status Status { get; set; }

    public Booking Booking { get; set; }
}




