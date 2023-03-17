namespace GreenFood.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message = "Entity was not found!") : base(message)
        {
        }
    }
}
