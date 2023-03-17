namespace GreenFood.Domain.Exceptions
{
    public class CreatingCategoryException : Exception
    {
        public CreatingCategoryException(string? message= "Incorrect category of product!") : base(message)
        {
        }
    }
}
