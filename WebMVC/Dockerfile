FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY ../*.sln .
COPY *.csproj ./WebMVC/
RUN dotnet restore

COPY . ./WebMVC/
WORKDIR /source/WebMVC
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebMVC.dll"]