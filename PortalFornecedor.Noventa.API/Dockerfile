#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PortalFornecedor.Noventa.API/PortalFornecedor.Noventa.API.csproj", "PortalFornecedor.Noventa.API/"]
RUN dotnet restore "PortalFornecedor.Noventa.API/PortalFornecedor.Noventa.API.csproj"
COPY . .
WORKDIR "/src/PortalFornecedor.Noventa.API"
RUN dotnet build "PortalFornecedor.Noventa.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortalFornecedor.Noventa.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortalFornecedor.Noventa.API.dll"]