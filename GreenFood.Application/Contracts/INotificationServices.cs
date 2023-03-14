namespace GreenFood.Application.Contracts
{
    public interface INotificationServices
    {
        Task DeleteLatestOrdersAsync(CancellationToken cancellationToken = default);
    }
}
