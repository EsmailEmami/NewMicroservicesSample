#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/Services/Catalog/Catalog.Api/Catalog.Api.csproj", "Src/Services/Catalog/Catalog.Api/"]
COPY ["Src/Services/Catalog/Catalog.Infrastructure/Catalog.Intrastructure.csproj", "Src/Services/Catalog/Catalog.Infrastructure/"]
COPY ["Src/Services/Catalog/Catalog.Application/Catalog.Application.csproj", "Src/Services/Catalog/Catalog.Application/"]
COPY ["Src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj", "Src/Services/Catalog/Catalog.Domain/"]
COPY ["Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/Infrastructure/"]
COPY ["Src/Application/Application.Core/Application.Core.csproj", "Src/Application/Application.Core/"]
COPY ["Src/Application/Application/Application.csproj", "Src/Application/Application/"]
COPY ["Src/Domain/Domain.Core/Domain.Core.csproj", "Src/Domain/Domain.Core/"]
COPY ["Src/Domain/Domain/Domain.csproj", "Src/Domain/Domain/"]
RUN dotnet restore "Src/Services/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Src/Services/Catalog/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]