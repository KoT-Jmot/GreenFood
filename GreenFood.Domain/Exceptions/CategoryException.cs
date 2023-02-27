namespace GreenFood.Domain.Exceptions
{
    public class CategoryException : Exception
    {
        public CategoryException(string? message= "Incorrect type of product!") : base(message)
        {
        }
    }
}
