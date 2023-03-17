namespace GreenFood.Domain.Exceptions
{
    public class UserRoleException : Exception
    {
        public UserRoleException(string message="Incorrect user role!") : base(message)
        {
        }
    }
}
