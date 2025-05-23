FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ../TechInventAPI/*.csproj TechInventAPI/
COPY ../TechInvent.BLL/*.csproj TechInvent.BLL/
COPY ../TechInvent.DAL/*.csproj TechInvent.DAL/
COPY ../TechInvent.DM/*.csproj TechInvent.DM/

WORKDIR /source/TechInventAPI
RUN dotnet restore

WORKDIR /source
COPY ../ ./

RUN dotnet publish TechInventAPI/TechInventAPI.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TechInventAPI.dll"]