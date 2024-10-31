FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ../*.sln .
COPY *.csproj ./TechInventAPI/
RUN dotnet restore

COPY . ./TechInventAPI/
WORKDIR /source/TechInventAPI
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "TechInventAPI.dll"]