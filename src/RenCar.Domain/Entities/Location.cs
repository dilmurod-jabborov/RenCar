namespace RenCar.Domain.Entities;

public class Location : AudiTable
{
    public string City { get; set; }
    public string Address { get; set; }

    public ICollection<CarLocation> CarLocations { get; set; }
}