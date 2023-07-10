FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GreenFood/GreenFood.Web.csproj", "GreenFood/"]
RUN dotnet restore "GreenFood/GreenFood.Web.csproj"
COPY . .
WORKDIR "/src/GreenFood"
RUN dotnet build "GreenFood.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GreenFood.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GreenFood.Web.dll"]