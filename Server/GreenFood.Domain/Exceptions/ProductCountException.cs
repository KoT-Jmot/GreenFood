namespace GreenFood.Domain.Exceptions
{
    public class ProductCountException : Exception
    {
        public ProductCountException(string? message = "Incorrect count of product!") : base(message)
        {
        }
    }
}
