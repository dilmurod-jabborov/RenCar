using System.ComponentModel.DataAnnotations.Schema;

namespace RenCar.Domain.Entities;

public class Car : AudiTable
{
    public int CompanyId { get; set; }
    public string Brand {  get; set; }
    public string Model { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerDay { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PledgePrice {  get; set; }
    public bool Available { get; set; }
    public int PickUpLocationId { get; set; }

    public Location PickUpLocation { get; set; }
    public RentCompany RentCompany { get; set; }
    public CarDetails CarDetails { get; set; }
    public ICollection<CarLocation> DropOffLocations { get; set; }
}
