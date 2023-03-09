namespace GreenFood.Application.Contracts
{
    public interface IAccountForAdminService
    {
        Task<string> BlockUserById(string userId);
        Task<string> SetAdminByUserID(string userId);
    }
}
