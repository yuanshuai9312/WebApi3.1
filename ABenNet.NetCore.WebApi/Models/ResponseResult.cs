using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABenNet.NetCore.WebApi.Models
{
    /// <summary>
    /// api返回T结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseResult<T>
    {
        /// <summary>
        /// 消息编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>

        public string Message { get; set; } = string.Empty;


        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// api返回结果
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// 消息编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>

        public string Message { get; set; } = string.Empty;

    }
    /// <summary>
    /// api返回结果状态码
    /// </summary>
    public enum ResponseResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Failure = 10,
        /// <summary>
        /// 请求失败
        /// </summary>
        RequestFailure = 11,
        /// <summary>
        /// 权限不足
        /// </summary>
        NoAuthority = 12,
        /// <summary>
        /// Token验证失败
        /// </summary>
        NoAuthorization = 13,
        /// <summary>
        /// 数据库操作失败
        /// </summary>
        DBChangeFailure = 14,
        /// <summary>
        /// Token失效
        /// </summary>
        TokenFailure = 15,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParameterError = 100,
        /// <summary>
        /// 参数值为空
        /// </summary>
        EmptyParameter = 101,
        /// <summary>
        /// 数据不存在
        /// </summary>
        Empty = 102,
        /// <summary>
        /// 用户已警用
        /// </summary>
        UserDisabled = 103,
    }
}
