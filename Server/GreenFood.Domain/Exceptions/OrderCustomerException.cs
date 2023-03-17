namespace GreenFood.Domain.Exceptions
{
    public class OrderCustomerException : Exception
    {
        public OrderCustomerException(string message="Incorrect customer of order!") : base(message) 
        { 
        }
    }
}
