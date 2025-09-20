using RenCar.Domain.Enums;

namespace RenCar.Service.Services.Users.Models;

public class UserUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Gender Gender{ get; set; }
    public string Email { get; set; }
    public string PassportNumber { get; set; }
}
