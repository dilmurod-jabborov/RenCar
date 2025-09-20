namespace RenCar.Service.Services.Cars.Models;

public class CarViewModel
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal PledgePrice { get; set; }
    public bool Available { get; set; }
    public CarDetailInfo CarDetailInfos { get; set; }

    public class CarDetailInfo
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
    }
}