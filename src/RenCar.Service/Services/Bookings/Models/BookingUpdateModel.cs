namespace RenCar.Service.Services.Bookings.Models;

public class BookingUpdateModel
{ 
    public int PickUpLocationId { get; set; }
    public int DropUpLocationId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}
