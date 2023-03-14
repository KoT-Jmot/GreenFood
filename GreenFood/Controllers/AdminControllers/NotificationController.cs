﻿using GreenFood.Application.Contracts;
using GreenFood.Domain.Utils;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers.AdminControllers
{
    [Authorize(Roles = AccountRoles.GetSuperAdministratorRole)]
    [Route("Admin/Notification")]
    public class NotificationController : Controller
    {
        private readonly INotificationServices _notificationManager;

        public NotificationController(INotificationServices notificationManager)
        {
            _notificationManager = notificationManager;
        }

        [Route("Orders")]
        public  IActionResult AddRecurringOrdersOperations(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate(() =>
                _notificationManager.DeleteLatestOrdersAsync(cancellationToken), Cron.Daily);

            return Ok();
        }

        
    }
}
