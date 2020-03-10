using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABenNet.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ABenNet.NetCore.WebApi.Controllers.v1
{
    /// <summary>
    /// 天气预报1.0
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")] //[Route("api/weatherforecast/[action]")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]

    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取天气预报详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<IEnumerable<WeatherForecast>> Get()
        {
            ResponseResult<IEnumerable<WeatherForecast>> responseResult = new ResponseResult<IEnumerable<WeatherForecast>>();
            var rng = new Random();
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Version = "v1",
            }).ToArray();
            responseResult.Code = (int)ResponseResultCode.Success;
            responseResult.Data = data.ToList();
            return responseResult;
        }

        [HttpGet]
        public ResponseResult Error()
        {
            ResponseResult responseResult = new ResponseResult();
            try
            {
                //throw new Exception("this is error");
                Console.WriteLine("正在处理数据，请耐心等待...");
                responseResult.Code = (int)ResponseResultCode.Success;
                responseResult.Message = "数据提交成功！";
            }
            catch (Exception ex)
            {
                responseResult.Code = (int)ResponseResultCode.Failure;
                responseResult.Message = ex.Message;
            }
            return responseResult;
        }
    }
}
