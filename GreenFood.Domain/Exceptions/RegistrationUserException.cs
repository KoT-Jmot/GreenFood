namespace GreenFood.Domain.Exceptions
{
    public class RegistrationUserException : Exception
    {
        public RegistrationUserException(
            string message = "Can't insert this object.")
            : base(message)
        {
        }
    }
}
