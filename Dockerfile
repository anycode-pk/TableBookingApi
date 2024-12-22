FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TableBooking.Api/TableBooking.Api.csproj", "TableBooking.Api/"]
COPY ["TableBooking.Logic/TableBooking.Logic.csproj", "TableBooking.Logic/"]
COPY ["TableBooking.Model/TableBooking.Model.csproj", "TableBooking.Model/"]
RUN dotnet restore "TableBooking.Api/TableBooking.Api.csproj"
COPY . .
WORKDIR "/src/TableBooking.Api"
RUN dotnet build "TableBooking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TableBooking.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM publish AS dbupdate
WORKDIR /app/publish
RUN dotnet ef database update --no-build

FROM base AS final
WORKDIR /app
COPY --from=dbupdate /app/publish .
ENTRYPOINT ["dotnet", "TableBooking.Api.dll"]
