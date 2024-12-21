FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TableBooking.Api/TableBooking.Api.csproj", "TableBooking.Api/"]
RUN dotnet restore "TableBooking.Api/TableBooking.Api.csproj"
COPY . .
WORKDIR "/src/TableBooking.Api"
RUN dotnet build "TableBooking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TableBooking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableBooking.dll"]