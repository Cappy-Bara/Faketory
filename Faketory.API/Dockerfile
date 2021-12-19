#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 102

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Faketory.API/Faketory.API.csproj", "Faketory.API/"]
COPY ["Faketory.Infrastructure/Faketory.Infrastructure.csproj", "Faketory.Infrastructure/"]
COPY ["Faketory.Domain/Faketory.Domain.csproj", "Faketory.Domain/"]
COPY ["Faketory.Application/Faketory.Application.csproj", "Faketory.Application/"]
RUN dotnet restore "Faketory.API/Faketory.API.csproj"
COPY . .
WORKDIR "/src/Faketory.API"
RUN dotnet build "Faketory.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Faketory.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Faketory.API.dll"]