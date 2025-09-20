using RenCar.Domain.Enums;

namespace RenCar.Domain.Entities;

public class BookDetails : AudiTable
{
    public int PickUpLocationId { get; set; }
    public int DropUpLocationId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public Status Status { get; set; }

    public Booking Booking { get; set; }
    public int BookingId { get; set; }
    public Location PickUpLocation { get; set; } 
    public Location DropUpLocation { get; set; } 
}




