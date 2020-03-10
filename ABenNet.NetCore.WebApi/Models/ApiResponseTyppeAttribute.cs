using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ABenNet.NetCore.WebApi.Models
{
    /// <summary>
    /// 接口成功时的输出模型定义
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ApiResponseTyppeAttribute : ProducesResponseTypeAttribute
    {
        public ApiResponseTyppeAttribute(Type type) : base(type, StatusCodes.Status200OK) { }
    }
}
