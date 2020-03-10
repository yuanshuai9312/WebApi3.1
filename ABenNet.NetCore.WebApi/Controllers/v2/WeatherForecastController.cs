using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABenNet.NetCore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace ABenNet.NetCore.WebApi.Controllers.v2
{
    /// <summary>
    /// 天气预报2.0
    /// </summary>
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class WeatherForecastController : BaseApiController //继承基类
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) : base(logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取天气预报详细信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiResponseTyppeAttribute(typeof(ResponseResult<IEnumerable<WeatherForecast>>))]
        public IActionResult Get()
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                Version = "v2",
            })
            .ToArray();
            return Ok(data);
            //return Ok(ResponseResultCode.Success,"处理成功",data);
        }

        /// <summary>
        /// SubmitByFromQueryParams多简单类型参数
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="ordername"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        [ApiResponseTyppeAttribute(typeof(ResponseResult<>))]
        public IActionResult SubmitByFromQueryParams(string orderid, string ordername)
        {
            return Ok(ResponseResultCode.Success, message: $"orderid={orderid},ordername={ordername}");
        }

        /// <summary>
        /// SubmitByFromBody复杂参数传递
        /// </summary>
        /// <param name="id"></param>
        /// <param name="weatherForecast"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiResponseTyppeAttribute(typeof(ResponseResult<WeatherForecast>))]
        public IActionResult SubmitByFromBody([FromQuery]int id, [FromBody]WeatherForecast weatherForecast)
        {
            weatherForecast.Version = id.ToString();
            return Ok(weatherForecast);
        }

        /// <summary>
        /// 这是一个测试错误的方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiResponseTyppeAttribute(typeof(ResponseResult))]
        public IActionResult Error()
        {
            int a = 10;
            int b = 0;
            int c = a / b;
            //throw new Exception("this is error");
            Console.WriteLine("正在处理数据，请耐心等待...");
            return Ok(ResponseResultCode.Success, message: $"this is error");
        }
    }
}
