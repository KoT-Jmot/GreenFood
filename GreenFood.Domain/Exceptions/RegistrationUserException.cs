namespace GreenFood.Domain.Exceptions
{
    public class RegistrationUserException : Exception
    {
        public RegistrationUserException(
            string message = "Can't registrate user!")
            : base(message)
        {
        }
    }
}
