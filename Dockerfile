# Use the official .NET 8.0 runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8.0 SDK as a build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TableBooking.Api/TableBooking.Api.csproj", "TableBooking.Api/"]
COPY ["TableBooking.Logic/TableBooking.Logic.csproj", "TableBooking.Logic/"]
COPY ["TableBooking.Model/TableBooking.Model.csproj", "TableBooking.Model/"]
RUN dotnet restore "TableBooking.Api/TableBooking.Api.csproj"
COPY . .
WORKDIR "/src/TableBooking.Api"
RUN dotnet build "TableBooking.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "TableBooking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: use the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableBooking.Api.dll"]
