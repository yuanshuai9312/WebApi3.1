using ABenNet.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABenNet.NetCore.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger Logger;
        public BaseApiController(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// 以<see cref="ResponseResult{T}"/>数据结构的Json输出API结果
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public new IActionResult Ok()
        {
            return Success(true);
        }

        /// <summary>
        /// 以<see cref="ResponseResult{T}"/>数据结构的Json输出API结果
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [NonAction]
        public new IActionResult Ok(object value)
        {
            return Success(value);
        }

        /// <summary>
        /// 以<see cref="ResponseResult{T}"/>数据结构的Json输出操作成功的API结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="details"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Success<T>(T details)
        {
            return Ok(ResponseResultCode.Success, string.Empty, details);
        }

        /// <summary>
        /// </summary>
        /// <param name="responseResultCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok(ResponseResultCode responseResultCode, string message)
        {
            var result = new ResponseResult<Object>
            {
                Code = (int)responseResultCode,
                Message = message,
                Data = null
            };
            return Ok(result);
        }

        /// <summary>
        /// 以<see cref="ResponseResult{T}"/>数据结构的Json输出API结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="responseResultCode"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok<T>(ResponseResultCode responseResultCode, string message, T details)
        {
            var result = new ResponseResult<T>
            {
                Code = (int)responseResultCode,
                Message = message,
                Data = details
            };
            return Ok(result);
        }

        /// <summary>
        /// 以<see cref="ResponseResult{T}"/>数据结构的Json输出API结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        [NonAction]
        public IActionResult Ok<T>(ResponseResult<T> result)
        {
            if (result == null)
            {
                result = new ResponseResult<T>
                {
                    Code = (int)ResponseResultCode.Success,
                    Message = string.Empty,
                    Data = default(T)
                };
            }
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            jsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.ContractResolver = new DefaultContractResolver();
            string data = JsonConvert.SerializeObject(result, Formatting.Indented, jsonSerializerSettings);
            HttpContext.Response.ContentType = "application/json;charset=utf-8;";
            return Content(data);
        }
    }
}
