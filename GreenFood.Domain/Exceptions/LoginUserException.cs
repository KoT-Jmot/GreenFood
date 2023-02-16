namespace GreenFood.Domain.Exceptions
{
    public class LoginUserException : Exception
    {
        public LoginUserException(
            string message = "Incorrect Email or Password!") 
            : base(message)
        {
        }
    }
}
