FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ../WebMVC/*.csproj WebMVC/
COPY ../TechInvent.BLL/*.csproj TechInvent.BLL/
COPY ../TechInvent.DAL/*.csproj TechInvent.DAL/
COPY ../TechInvent.DM/*.csproj TechInvent.DM/

WORKDIR /source/WebMVC
RUN dotnet restore

WORKDIR /source
COPY ../ ./

RUN dotnet publish WebMVC/WebMVC.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebMVC.dll"]
