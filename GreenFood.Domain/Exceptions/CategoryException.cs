namespace GreenFood.Domain.Exceptions
{
    public class CategoryException : Exception
    {
        public CategoryException(string? message= "Incorrect category of product!") : base(message)
        {
        }
    }
}
