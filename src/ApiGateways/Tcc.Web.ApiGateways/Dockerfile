#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateways/Tcc.Web.ApiGateways/Tcc.Web.ApiGateways.csproj", "ApiGateways/Tcc.Web.ApiGateways/"]
RUN dotnet restore "ApiGateways/Tcc.Web.ApiGateways/Tcc.Web.ApiGateways.csproj"
COPY . .
WORKDIR "/src/ApiGateways/Tcc.Web.ApiGateways"
RUN dotnet build "Tcc.Web.ApiGateways.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tcc.Web.ApiGateways.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tcc.Web.ApiGateways.dll"]