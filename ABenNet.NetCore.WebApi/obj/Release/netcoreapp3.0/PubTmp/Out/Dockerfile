FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
COPY . .

#配置ASP.NET CORE环境变量
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"
EXPOSE 5000

ENTRYPOINT ["dotnet", "ABenNet.NetCore.WebApi.dll"]