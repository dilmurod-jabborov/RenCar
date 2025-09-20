namespace RenCar.Domain.Entities;

public class CarLocation : AudiTable
{
    public int CarId { get; set; }
    public int LocationId { get; set; }

    public Car Car { get; set; }
    public Location Location { get; set; }
}
