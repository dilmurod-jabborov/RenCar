namespace RenCar.Service.Exeptions;

public class AlreadyExistException : Exception
{
    private int StatusCode;
    public AlreadyExistException(string message) : base(message)
    {
        StatusCode = 403;
    }
}
