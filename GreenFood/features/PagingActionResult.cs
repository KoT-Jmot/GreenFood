﻿using GreenFood.Application.Contracts;
using GreenFood.Application.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
namespace GreenFood.Web.features
{
    public class PagingActionResult<T> : IActionResult where T : IPagination
    {
        private readonly T _result;

        public PagingActionResult(T result) {
            _result = result; 
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var headers = new Dictionary<string, StringValues>
            {
                {ResponceHeaders.CurrentPage, _result.MetaData!.CurrentPage.ToString()},
                {ResponceHeaders.TotalCount, _result.MetaData.TotalCount.ToString()},
                {ResponceHeaders.TotalPages, _result.MetaData.TotalPages.ToString()},
                {ResponceHeaders.PageSize, _result.MetaData.PageSize.ToString()},
                {ResponceHeaders.HasNext, _result.MetaData.HasNext.ToString()},
                {ResponceHeaders.HasPrevious, _result.MetaData.HasPrevious.ToString()}

            };

            foreach (var item in headers)
            {
                context.HttpContext.Response.Headers.Add(item);
            }

            var objResult = new ObjectResult(_result)
            {
                StatusCode = StatusCodes.Status200OK
            };

            await objResult.ExecuteResultAsync(context);
        }
    }
}
