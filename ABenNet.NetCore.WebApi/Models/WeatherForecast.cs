using System;

namespace ABenNet.NetCore.WebApi.Models
{
    /// <summary>
    /// ����Ԥ��ʵ��
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// /��������
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ����C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ����F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ��Ҫ
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// �汾��
        /// </summary>
        public string Version { get; set; }
    }
}
