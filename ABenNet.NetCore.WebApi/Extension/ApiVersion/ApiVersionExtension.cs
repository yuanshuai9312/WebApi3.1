using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ABenNet.NetCore.WebApi.Extension.ApiVersion
{
    public static class ApiVersionExtension
    {
        /// <summary>
        /// Add api version support
        /// </summary>
        /// <param name="services">The instance of <see cref="IServiceCollection"/></param>
        public static void AddApiVersion(this IServiceCollection services)
        {
            // Add api version support
            services.AddApiVersioning(o =>
            {
                // return api version info in response header
                // ReportAPIVersions: 这是可选的。但是, 当设置为 true 时, API 将返回响应标头中支持的版本信息。
                o.ReportApiVersions = true;

                // default api version
                // AssumeDefaultVersionWhenUnspecified: 此选项将用于不提供版本的请求。默认情况下, 假定的 API 版本为1.0。
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

                // when not specifying an api version, select the default version
                //DefaultApiVersion: 此选项用于指定在请求中未指定版本时要使用的默认 API 版本。这将默认版本为1.0。
                o.AssumeDefaultVersionWhenUnspecified = true;

            });

            // Config api version info
            services.AddVersionedApiExplorer(option =>
            {
                // Set api version group name format
                option.GroupNameFormat = "'v'VVVV";

                // when not specifying an api version, select the default version
                option.AssumeDefaultVersionWhenUnspecified = true;
            });
        }
    }
}