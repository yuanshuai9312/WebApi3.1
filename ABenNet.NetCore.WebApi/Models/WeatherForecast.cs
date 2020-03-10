using System;

namespace ABenNet.NetCore.WebApi.Models
{
    /// <summary>
    /// 天气预报实体
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// /播报日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 气温C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 气温F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 概要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }
}
