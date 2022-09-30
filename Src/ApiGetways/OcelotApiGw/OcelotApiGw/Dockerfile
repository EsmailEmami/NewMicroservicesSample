#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Src/ApiGetways/OcelotApiGw/OcelotApiGw/OcelotApiGw.csproj", "Src/ApiGetways/OcelotApiGw/OcelotApiGw/"]
COPY ["Src/Infrastructure/Infrastructure/Infrastructure.csproj", "Src/Infrastructure/Infrastructure/"]
COPY ["Src/Application/Application.Core/Application.Core.csproj", "Src/Application/Application.Core/"]
COPY ["Src/Application/Application/Application.csproj", "Src/Application/Application/"]
COPY ["Src/Domain/Domain.Core/Domain.Core.csproj", "Src/Domain/Domain.Core/"]
COPY ["Src/Domain/Domain/Domain.csproj", "Src/Domain/Domain/"]
RUN dotnet restore "Src/ApiGetways/OcelotApiGw/OcelotApiGw/OcelotApiGw.csproj"
COPY . .
WORKDIR "/src/Src/ApiGetways/OcelotApiGw/OcelotApiGw"
RUN dotnet build "OcelotApiGw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGw.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGw.dll"]