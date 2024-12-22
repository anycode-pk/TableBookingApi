FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TableBooking.Api/TableBooking.Api.csproj", "TableBooking.Api/"]
COPY ["TableBooking.Logic/TableBooking.Logic.csproj", "TableBooking.Logic/"]
COPY ["TableBooking.Model/TableBooking.Model.csproj", "TableBooking.Model/"]
RUN dotnet restore "TableBooking.Api/TableBooking.Api.csproj"

COPY . .
WORKDIR "/src/TableBooking.Api"
RUN dotnet build "TableBooking.Api.csproj" -c Release -o /app/build

RUN dotnet tool install --global dotnet-ef --version 8.0.0

FROM build AS publish
RUN dotnet publish "TableBooking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet ef database update

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TableBooking.Api.dll"]
