namespace GreenFood.Application.Contracts
{
    public interface IAccountManagement
    {
        Task<string> BlockUserById(string userId);
        Task<string> SetAdminByUserID(string userId);
    }
}
