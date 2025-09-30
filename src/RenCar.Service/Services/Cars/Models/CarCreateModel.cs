using System.ComponentModel.DataAnnotations.Schema;

namespace RenCar.Service.Services.Cars.Models;

public class CarCreateModel
{
    public int CompanyId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal PledgePrice {  get; set; }
    public int PickUpLocationId { get; set; }
    public List<int> DropOffLocationIds {  get; set; }

    public int Year { get; set; }
    public int Seats { get; set; }
    public string FuelType { get; set; }
    public string Transmission { get; set; }
}
