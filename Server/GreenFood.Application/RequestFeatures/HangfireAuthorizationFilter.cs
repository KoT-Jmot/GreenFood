using GreenFood.Domain.Utils;
using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace GreenFood.Application.RequestFeatures
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.IsInRole(AccountRoles.GetSuperAdministratorRole);
        }
    }
}
