namespace GreenFood.Domain.Exceptions
{
    public class RequestAccessException : Exception
    {
        public RequestAccessException(string message= "Access was denied!") : base(message)
        {
        }
    }
}
