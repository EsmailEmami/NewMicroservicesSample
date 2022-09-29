#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/Basket/Basket.Api/Basket.Api.csproj", "Src/Services/Basket/Basket.Api/"]
COPY ["Src/Services/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "Src/Services/Basket/Basket.Infrastructure/"]
COPY ["Src/Services/Basket/Basket.Application/Basket.Application.csproj", "Src/Services/Basket/Basket.Application/"]
COPY ["Src/Services/Basket/Basket.Domain/Basket.Domain.csproj", "Src/Services/Basket/Basket.Domain/"]
COPY ["Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/Infrastructure/"]
COPY ["Src/Application/Application.Core/Application.Core.csproj", "Src/Application/Application.Core/"]
COPY ["Src/Application/Application/Application.csproj", "Src/Application/Application/"]
COPY ["Src/Domain/Domain.Core/Domain.Core.csproj", "Src/Domain/Domain.Core/"]
COPY ["Src/Domain/Domain/Domain.csproj", "Src/Domain/Domain/"]
COPY ["Src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "Src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "Src/Services/Basket/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/Basket/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]