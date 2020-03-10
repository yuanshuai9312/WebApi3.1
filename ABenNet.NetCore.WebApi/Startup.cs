using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABenNet.NetCore.WebApi.Extension.ApiVersion;
using ABenNet.NetCore.WebApi.Extension.Swagger;
using ABenNet.NetCore.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ABenNet.NetCore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Config cors policy
            services.AddCors();

            // Config api version
            services.AddApiVersion();

            // Add swagger api doc support
            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            //}

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            //app.UseHttpsRedirection();//一般实际开发中我们都是通过反向代理服务器nginx来配置开启Https

            app.UseRouting();

            // Allow cross domain request
            //CORS 中间件必须配置为在对UseRouting和UseEndpoints的调用之间执行
            // 允许所有的来源地址跨域访问并且支持将cookie发送到服务端。 
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Enable swagger doc
            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                // Default load the latest version
                foreach (var description in provider.ApiVersionDescriptions.Reverse())
                {
                    s.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        $"ABenNetCoe在线接口API文档 {description.GroupName.ToUpperInvariant()}");
                }
            });
        }
    }
}
