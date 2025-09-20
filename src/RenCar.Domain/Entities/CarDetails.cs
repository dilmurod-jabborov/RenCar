namespace RenCar.Domain.Entities;

public class CarDetails : AudiTable
{
    public int CarId {  get; set; }
    public int Year { get; set; }
    public int Seats { get; set; }
    public string FuelType { get; set; }
    public string Transmission { get; set; }
    public string Pictures { get; set; } = null;

    public Car Car { get; set; }
}
    

