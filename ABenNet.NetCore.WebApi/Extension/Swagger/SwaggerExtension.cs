using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ABenNet.NetCore.WebApi.Extension.Swagger
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            // Config swagger doc info
            services.AddSwaggerGen(s =>
            {
                // Generate api doc by api version info
                //
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    s.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Contact = new OpenApiContact
                        {
                            Name = "ABenNet ASP.NET Core WebApi",
                            Email = "abennt@163.com",
                            Url = new Uri("https://abennet.com")
                        },
                        Description = "ABenNetCoe.WebAPI.ApiVersion 接口文档",
                        Title = "ABenNetCoe.WebAPI.ApiVersion",
                        Version = description.ApiVersion.ToString()
                    });
                }

                // Show api version in the url which swagger doc generated
                s.DocInclusionPredicate((version, apiDescription) =>
                {
                    if (!version.Equals(apiDescription.GroupName))
                        return false;

                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", apiDescription.GroupName));

                    apiDescription.RelativePath = string.Join("/", values);
                    return true;
                });

                // Let params use the camel naming method
                s.DescribeAllParametersInCamelCase();

                // Remove version param must input in swagger doc
                s.OperationFilter<RemoveVersionFromParameter>();

                // Get project's api description file
                var basePath = Path.GetDirectoryName(AppContext.BaseDirectory);
                var apiPath = Path.Combine(basePath, "ABenNet.NetCore.WebApi.xml");
                if (File.Exists(apiPath)) s.IncludeXmlComments(apiPath, true);

                //为 Swagger JSON and UI设置xml文档注释路径
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //s.IncludeXmlComments(xmlPath, true);
            });
        }
    }
}