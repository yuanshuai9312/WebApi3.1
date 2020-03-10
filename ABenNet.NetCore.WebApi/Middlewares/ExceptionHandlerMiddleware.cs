using ABenNet.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ABenNet.NetCore.WebApi.Middlewares
{
    /// <summary>
    /// 封装ExceptionHandlerMiddeware中间件，拦截程序报错，返回自定义实体。
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
                
                //注意检测Response状态码（是否为404）和Redirect跳转的逻辑都是写在await _next.Invoke(context)之后的。
                if (context.Response.StatusCode == 404)
                {
                    context.Response.ContentType = "application/json";
                    var responseMessage = new ResponseResult<string>
                    {
                        Code = (int)ResponseResultCode.RequestFailure,
                        Message = "对不起，请求的URL接口地址不存在，请仔细检查！",
                        Data = string.Empty,
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(responseMessage)).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            if (e == null) return;
            await WriteExceptionAsync(context, e).ConfigureAwait(false);
        }
        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException)
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";
            var responseMessage = new ResponseResult<string>
            {
                Message = e.GetBaseException().Message,
                Code = (int)ResponseResultCode.Failure,
                Data = string.Empty
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseMessage)).ConfigureAwait(false);
        }
    }
}
