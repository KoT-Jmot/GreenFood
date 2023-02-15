using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace GreenFood.Web.Exception
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(
                            this IApplicationBuilder app,
                            ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var ExceptionMessage = ($"Something went wrong: {contextFeature.Error}");
                        logger.LogError(ExceptionMessage);
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }

    }
}
