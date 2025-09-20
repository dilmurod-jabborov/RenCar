namespace RenCar.Service.Exeptions;

partial class NotFoundException : Exception
{
    private int StatusCode;
    public NotFoundException(string message) : base(message)
    {
        StatusCode = 404;
    }
}
