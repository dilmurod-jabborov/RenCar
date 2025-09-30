namespace RenCar.Service.Exeptions;

public class PaymentFailedException : Exception
{
    private int StatusCode;
    public PaymentFailedException(string message) : base(message)
    {
        StatusCode = 402;
    }
}
