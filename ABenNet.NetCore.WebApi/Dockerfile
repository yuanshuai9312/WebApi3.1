FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
COPY . .

#����ASP.NET CORE��������
ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="Development"
EXPOSE 5000

ENTRYPOINT ["dotnet", "ABenNet.NetCore.WebApi.dll"]