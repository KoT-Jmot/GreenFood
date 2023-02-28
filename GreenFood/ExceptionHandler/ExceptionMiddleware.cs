﻿using FluentValidation;
using GreenFood.Domain.Exceptions;

namespace GreenFood.Web.ExceptionHandler
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, DateTime.UtcNow);

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);

            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message,
            }.ToString());
        }

        private int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                LoginUserException => StatusCodes.Status422UnprocessableEntity,
                RegistrationUserException => StatusCodes.Status422UnprocessableEntity,
                CategoryException => StatusCodes.Status422UnprocessableEntity,
                ArgumentNullException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError,
            };
        }
    }
}
