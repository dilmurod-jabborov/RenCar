using RenCar.Domain.Enums;

namespace RenCar.Domain.Entities;

public class User : AudiTable
{
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public string Phone {  get; set; }
    public string PassportNumber { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}


