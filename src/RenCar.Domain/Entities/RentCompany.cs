namespace RenCar.Domain.Entities;

public class RentCompany : AudiTable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Phone { get; set; }
    public float Rating { get; set; }

    public ICollection<Car> Cars { get; set; }
}
